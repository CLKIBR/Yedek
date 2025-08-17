from typing import Dict, Any, Tuple, Optional
import logging
import numpy as np
import pandas as pd
from src.strategy.strategy_base import StrategyBase
from src.utils.indicators import (
    calculate_rsi, calculate_macd, calculate_bollinger_bands, calculate_ema, calculate_supertrend
)

logger = logging.getLogger("binance_bot")

class ExampleStrategy(StrategyBase):
    """
    Gelişmiş indikatörler kullanarak sinyal üreten strateji:
    - RSI, MACD, Bollinger Bands, EMA, Supertrend kombinasyonu.
    - Sinyalin güven skoru (confidence) döner.
    """

    def __init__(self, client, params: Dict[str, Any]):
        super().__init__(client, params)
        self.symbol = params.get("symbol", "BTCUSDT")
        self.interval = params.get("interval", "1m")
        self.quantity = params.get("quantity", 0.001)

    def _get_klines(self, limit: int = 100) -> pd.DataFrame:
        klines = self.client.get_klines(self.symbol, self.interval, limit=limit)
        df = pd.DataFrame({
            'Open': [float(k[1]) for k in klines],
            'High': [float(k[2]) for k in klines],
            'Low': [float(k[3]) for k in klines],
            'Close': [float(k[4]) for k in klines],
            'Volume': [float(k[5]) for k in klines],
        })
        return df

    def generate_signal(self) -> Dict[str, Any]:
        """
        İlgili klines verilerini çek, gelişmiş indikatörleri hesapla ve sinyal ile confidence döndür.
        """

        try:
            df = self._get_klines(limit=100)
            closes = df['Close'].values

            # İndikatörleri hesapla
            rsi = calculate_rsi(closes)
            macd_line, signal_line = calculate_macd(closes)
            upper_band, lower_band, last_close = calculate_bollinger_bands(closes)
            ema = calculate_ema(closes)
            supertrend_df = calculate_supertrend(df)

            # Supertrend sinyalini al
            supertrend_signal: Optional[str] = None
            if not supertrend_df.empty:
                if supertrend_df['supertrend'].iloc[-1]:
                    supertrend_signal = "BUY"
                else:
                    supertrend_signal = "SELL"

            if not supertrend_signal:
                logger.info(f"{self.symbol}: Supertrend sinyali yok, HOLD pozisyonu.")
                return {
                    "action": "HOLD",
                    "quantity": 0,
                    "price": None,
                    "reason": "Supertrend sinyali yok",
                    "confidence": 0.0
                }

            # Destekleyici sinyaller
            rsi_buy = rsi is not None and rsi < 30
            macd_buy = macd_line > signal_line
            bollinger_buy = last_close < lower_band
            ema_buy = last_close > ema

            rsi_sell = rsi is not None and rsi > 70
            macd_sell = macd_line < signal_line
            bollinger_sell = last_close > upper_band
            ema_sell = last_close < ema

            if supertrend_signal == "BUY":
                support_signals = [rsi_buy, macd_buy, bollinger_buy, ema_buy]
            else:  # SELL
                support_signals = [rsi_sell, macd_sell, bollinger_sell, ema_sell]

            support_count = sum(support_signals)

            if support_count >= 0:
                confidence = min(1.0 + support_count * 0.25, 2.0)
                action = supertrend_signal
                reason = f"Supertrend sinyali + {support_count} destekleyici sinyal"
            else:
                confidence = 0.0
                action = "HOLD"
                reason = "Destekleyici sinyaller yetersiz"

            logger.info(f"{self.symbol} Sinyal: {action} | Neden: {reason} | Confidence: {confidence:.2f}")

            return {
                "action": action,
                "quantity": self.quantity if action in ["BUY", "SELL"] else 0,
                "price": None,
                "reason": reason,
                "confidence": confidence
            }

        except Exception as e:
            logger.error(f"{self.symbol} için strateji sinyali oluşturulamadı: {e}")
            return {
                "action": "HOLD",
                "quantity": 0,
                "price": None,
                "reason": f"Hata: {e}",
                "confidence": 0.0
            }
