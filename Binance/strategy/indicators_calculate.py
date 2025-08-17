from data.klines import klines
import pandas as pd
import ta.trend
import ta.momentum
import ta.volatility
import ta.volume

def get_signal_icon(score):
    if score > 0:
        return "🟢"
    elif score < 0:
        return "🔴"
    else:
        return "⚪"

def calculate_momentum_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0

        # Stochastic RSI
        stoch_rsi_indikator = ta.momentum.StochRSIIndicator(kl['Close'], 14, 3, 3)
        stoch_rsi_k = stoch_rsi_indikator.stochrsi_k().iloc[-1] * 100
        stoch_rsi_d = stoch_rsi_indikator.stochrsi_d().iloc[-1] * 100
        fark = stoch_rsi_k - stoch_rsi_d

        if stoch_rsi_k < 20 and fark >= 5:
            score += 100
        elif stoch_rsi_k > 80 and fark <= -5:
            score -= 100

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Momentum Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Momentum Hatası: {e}")
        return 0

def calculate_volatility_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0
        open = kl['Open'].iloc[-1]

        # Keltner Channel
        kc = ta.volatility.KeltnerChannel(kl['High'], kl['Low'], kl['Close'], 20, 10,multiplier=2, original_version=False)
        kc_hband = kc.keltner_channel_hband().iloc[-1]
        kc_lband = kc.keltner_channel_lband().iloc[-1]

        if open < kc_lband:
            score += 100
        elif open > kc_hband:
            score -= 100

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Volatility Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Volatility Hatası: {e}")
        return 0

def calculate_supertrend_indicator(symbol: str) -> int:
    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0
        open = kl['Open'].iloc[-1]

        # Supertrend hesaplama (14 periyot, 3 ATR)
        atr_period = 14
        atr_multiplier = 3.0
        supertrend = ta.trend.STCIndicator(
            close=kl['Open'],
            fillna=False
        )  # Alternatif: kendi supertrend implementasyonunuzu da yazabilirsiniz.

        # Eğer ta kütüphanenizde STCIndicator uygun değilse aşağıdaki gibi manuel Supertrend hesaplanabilir:
        atr = ta.volatility.AverageTrueRange(kl['High'], kl['Low'], kl['Close'], window=atr_period).average_true_range()
        hl2 = (kl['High'] + kl['Low']) / 2
        upper_band = hl2 + (atr_multiplier * atr)
        lower_band = hl2 - (atr_multiplier * atr)

        # Trend yönü belirleme
        if open > upper_band.iloc[-1]:
            score += 100  # Güçlü yükseliş trendi
        elif open < lower_band.iloc[-1]:
            score -= 100  # Güçlü düşüş trendi
        else:
            score += 0  # Nötr/Geçiş bölgesi

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Supertrend Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Supertrend Hatası: {e}")
        return 0

def calculate_trend_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 60:
            return 0

        score = 0
        ema20 = ta.trend.EMAIndicator(kl['Close'], window=20).ema_indicator().iloc[-1]
        ema50 = ta.trend.EMAIndicator(kl['Close'], window=50).ema_indicator().iloc[-1]
        if ema20 > ema50:
            score += 20
        else:
            score -= 20

        adx = ta.trend.ADXIndicator(kl['High'], kl['Low'], kl['Close'], window=14)
        adx_val = adx.adx().iloc[-1]
        plus_di = adx.adx_pos().iloc[-1]
        minus_di = adx.adx_neg().iloc[-1]

        if adx_val > 35:
            if plus_di > minus_di:
                score += 20  # Güçlü yukarı trend → Long
            elif minus_di > plus_di:
                score -= 20  # Güçlü aşağı trend → Short
            else:
                score += 0   # Net yön yok
        else:
            score += 0  # Trend zayıf, işlem önerilmez

        stc_vals = ta.trend.STCIndicator(kl['Close']).stc()
        stc_current = stc_vals.iloc[-1]
        stc_prev = stc_vals.iloc[-2]

        # STC yükseliyor ve > 50 → güçlü long sinyali
        if stc_current > 50 and stc_current > stc_prev:
            score += 20
        # STC düşüyor ve < 50 → güçlü short sinyali
        elif stc_current < 50 and stc_current < stc_prev:
            score -= 20
        # Net olmayan bölgeler → puan verilmez

        psar_vals = ta.trend.PSARIndicator(kl['High'], kl['Low'], kl['Close']).psar()
        psar_now = psar_vals.iloc[-1]
        psar_prev = psar_vals.iloc[-2]
        price_now = kl['Close'].iloc[-1]
        price_prev = kl['Close'].iloc[-2]

        if psar_now < price_now and psar_prev < price_prev:
            score += 20  # İstikrarlı yükseliş trendi
        elif psar_now > price_now and psar_prev > price_prev:
            score -= 20  # İstikrarlı düşüş trendi
        else:
            score += 0  # Yeni kesişim olmuş olabilir, kararsız bölge

        trix_indicator = ta.trend.TRIXIndicator(kl['Close'])
        trix_val = trix_indicator.trix().iloc[-1]
        trix_prev = trix_indicator.trix().iloc[-2]

        if trix_val > 0 and trix_val > trix_prev:
            score += 20  # Pozitif momentum ve artış → güçlü long sinyali
        elif trix_val < 0 and trix_val < trix_prev:
            score -= 20  # Negatif momentum ve azalış → güçlü short sinyali
        else:
            score += 0  # Zayıf, kararsız sinyal bölgesi

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Trend Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Trend Hatası: {e}")
        return 0


    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0

        obv_series = ta.volume.OnBalanceVolumeIndicator(kl['Close'], kl['Volume']).on_balance_volume()
        obv = obv_series.iloc[-1]
        obv_prev = obv_series.iloc[-2]
        price = kl['Close'].iloc[-1]
        price_prev = kl['Close'].iloc[-2]        

        if obv > obv_prev and price > price_prev:
            score += 25  # Hacim ve fiyat birlikte yükseliyor → güçlü long sinyali
        elif obv < obv_prev and price < price_prev:
            score -= 25  # Hacim ve fiyat birlikte düşüyor → güçlü short sinyali
        else:
            score += 0   # Uyumsuzluk → sinyal zayıf veya kararsız

        nvi_series = ta.volume.NegativeVolumeIndexIndicator(kl['Close'], kl['Volume']).negative_volume_index()

        if nvi_series.iloc[-1] > nvi_series.iloc[-2] > nvi_series.iloc[-3]:
            score += 25  # NVI sürekli artıyor → pozitif sinyal
        elif nvi_series.iloc[-1] < nvi_series.iloc[-2] < nvi_series.iloc[-3]:
            score -= 25  # NVI sürekli düşüyor → negatif sinyal
        else:
            score += 0   # Kararsız veya yatay

        pvo_indicator = ta.momentum.PercentageVolumeOscillator(kl['Volume'])
        pvo = pvo_indicator.pvo().iloc[-1]
        pvo_signal = pvo_indicator.pvo_signal().iloc[-1]

        if pvo > pvo_signal:
            score += 25  # Hacim momentumu artıyor → pozitif sinyal
        elif pvo < pvo_signal:
            score -= 25  # Hacim momentumu zayıflıyor → negatif sinyal
        else:
            score += 0   # Nötr


        cmf = ta.volume.ChaikinMoneyFlowIndicator(kl['High'], kl['Low'], kl['Close'], kl['Volume']).chaikin_money_flow().iloc[-1]
        if cmf > 0.05:
            score += 25  # Güçlü alım baskısı
        elif cmf < -0.05:
            score -= 25  # Güçlü satış baskısı
        else:
            score += 0   # Nötr bölge, kararsızlık
            
        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Volume Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Volume Hatası: {e}")
        return 0

def calculate_volume_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0
        price = kl['Close'].iloc[-1]
        price_prev = kl['Close'].iloc[-2]

        # OBV ve fiyat uyumlu mu?
        obv_series = ta.volume.OnBalanceVolumeIndicator(kl['Close'], kl['Volume']).on_balance_volume()
        obv = obv_series.iloc[-1]
        obv_prev = obv_series.iloc[-2]

        if obv > obv_prev and price > price_prev:
            score += 25
        elif obv < obv_prev and price < price_prev:
            score -= 25

        # OBV uyumsuzluğu kontrolü
        if price > price_prev and obv < obv_prev:
            score -= 25  # Hacim desteklemiyor → düşüş riski

        # NVI trendi
        nvi_series = ta.volume.NegativeVolumeIndexIndicator(kl['Close'], kl['Volume']).negative_volume_index()
        if nvi_series.iloc[-1] > nvi_series.iloc[-2] > nvi_series.iloc[-3]:
            score += 25
        elif nvi_series.iloc[-1] < nvi_series.iloc[-2] < nvi_series.iloc[-3]:
            score -= 25

        # PVO
        pvo_indicator = ta.momentum.PercentageVolumeOscillator(kl['Volume'])
        pvo = pvo_indicator.pvo().iloc[-1]
        pvo_signal = pvo_indicator.pvo_signal().iloc[-1]
        if pvo > pvo_signal:
            score += 25
        elif pvo < pvo_signal:
            score -= 25

        # CMF
        cmf = ta.volume.ChaikinMoneyFlowIndicator(kl['High'], kl['Low'], kl['Close'], kl['Volume']).chaikin_money_flow().iloc[-1]
        if cmf > 0.10:
            score += 25
        elif cmf < -0.10:
            score -= 25

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Volume Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Volume Hatası: {e}")
        return 0

def calculate_support_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol, "15m", 200)
        if kl is None or kl.empty or len(kl) < 200:
            return 0

        score = 0
        sma200 = ta.trend.SMAIndicator(kl['Close'], window=200).sma_indicator().iloc[-1]
        close = kl['Close'].iloc[-1]

        if close > sma200:
            score += 100
        else:
            score -= 100

        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Support Indicators Score: {score}")
        return score

    except Exception as e:
        print(f"[{symbol}] Support Hatası: {e}")
        return 0

def keltner_marubozu_score(symbol: str) -> int:
    try:
        kl = klines(symbol, "15m", 50)
        if kl is None or kl.empty or len(kl) < 50:
            return 0

        score = 0
        close = kl['Close'].iloc[-1]
        open_now = kl['Open'].iloc[-1]
        high_now = kl['High'].iloc[-1]
        low_now = kl['Low'].iloc[-1]

        body = abs(close - open_now)
        shadow_total = high_now - low_now

        # Keltner Bandı Hesabı
        kc = ta.volatility.KeltnerChannel(kl['High'], kl['Low'], kl['Close'], 20, 10, original_version=False)
        upper_band = kc.keltner_channel_hband().iloc[-1]
        lower_band = kc.keltner_channel_lband().iloc[-1]

        # Bearish Marubozu + Üst Band = Short Sinyali
        if (
            open_now > close and  # kırmızı mum
            shadow_total > 0 and (body / shadow_total) > 0.8 and  # marubozu
            close > upper_band  # üst banda taşmış
        ):
            score = -50
            icon = get_signal_icon(score)
            print(f"{icon} [{symbol}] Keltner + Bearish Marubozu = Short Skoru: {score}")
            return score

        # Bullish Marubozu + Alt Band = Long Sinyali
        if (
            close > open_now and  # yeşil mum
            shadow_total > 0 and (body / shadow_total) > 0.8 and  # marubozu
            close < lower_band  # alt banda taşmış
        ):
            score = 50
            icon = get_signal_icon(score)
            print(f"{icon} [{symbol}] Keltner + Bullish Marubozu = Long Skoru: {score}")
            return score

        return 0

    except Exception as e:
        print(f"[{symbol}] Keltner Marubozu Hatası: {e}")
        return 0

def super_scalper_indicators(symbol: str) -> int:
    try:
        kl = klines(symbol, limit=150)
        if kl is None or kl.empty or len(kl) < 100:
            return "NONE"

        score = 0

        # EMA hesapla
        EMA21 = ta.trend.EMAIndicator(kl['Close'], window=21).ema_indicator()
        EMA65 = ta.trend.EMAIndicator(kl['Close'], window=65).ema_indicator()

        # RSI hesapla
        RSI25 = ta.momentum.RSIIndicator(kl['Close'], window=25).rsi()
        RSI100 = ta.momentum.RSIIndicator(kl['Close'], window=100).rsi()

        # Son değerler
        RSILong = RSI25.iloc[-1] > RSI100.iloc[-1]
        RSIShort = RSI25.iloc[-1] < RSI100.iloc[-1]
        GoldenCross = EMA21.iloc[-1] > EMA65.iloc[-1] and EMA21.iloc[-2] <= EMA65.iloc[-2]
        DeathCross = EMA21.iloc[-1] < EMA65.iloc[-1] and EMA21.iloc[-2] >= EMA65.iloc[-2]

        if RSILong and GoldenCross:
            score += 100 
        elif RSIShort and DeathCross:
            score -= 100  
        
        icon = get_signal_icon(score)
        print(f"{icon} [{symbol}] Super Scalper İndicators: {score}")
        return score
    
    except Exception as e:
        print(f"[{symbol}] Momentum Hatası: {e}")
        return 0