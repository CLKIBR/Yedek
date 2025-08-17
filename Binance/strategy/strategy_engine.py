# strategy/strategy_engine.py
from strategy.indicators_calculate import (
    calculate_momentum_indicators,
    calculate_trend_indicators,
    calculate_volatility_indicators,
    calculate_volume_indicators,
    calculate_support_indicators,
    keltner_marubozu_score,
    super_scalper_indicators
)
from datetime import datetime
from utils.notifier import Notifier

class Super_Trend_Strategy:
    @staticmethod
    def generate_signal(symbol: str) -> str:
        scores = {
            "momentum": sum(filter(None, [calculate_momentum_indicators(symbol)])),
            "volatility": sum(filter(None, [calculate_volatility_indicators(symbol)])),
            #"scalper": sum(filter(None, [super_scalper_indicators(symbol)]))
        }
        trend_scores = {
            #"trend": sum(filter(None, [calculate_trend_indicators(symbol)])),
            #"volume": sum(filter(None, [calculate_volume_indicators(symbol)])),
            #"support": sum(filter(None, [calculate_support_indicators(symbol)])),
            #"marubozu": sum(filter(None, [keltner_marubozu_score(symbol)]))
        }

        # Momentum veya Volatility sıfırsa işlem yapma
        if scores["momentum"] == 0 or scores["volatility"] == 0:
            print(f"[{symbol}] Momentum veya Volatility eksik - işlem yapılmadı.")
            return "NONE"
        
        
        total_score = sum(scores.values()) // 2
        now = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        notifier = Notifier()
        message = (
            f"{now} | "
            f"Sembol: {symbol} | "
            f"Yön: {'LONG' if total_score >= 100 else 'SHORT' if total_score <= -100 else 'NONE' } | "
            f"Score: {total_score} | ")
        if total_score >= 100 or total_score <= -100 :
            notifier.send_message(message)
        
        print(f"[{symbol}] Total Score: {total_score}")

        if total_score >= 100:
            return "LONG"
        elif total_score <= -100:
            return "SHORT"
        else:
            return "NONE"
