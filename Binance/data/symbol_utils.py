from core.client import client, handle_client_error
import pandas as pd
import ta.volatility
import time

def get_futures_usdt_symbols():
    try:
        symbols = []
        info = client.exchange_info()
        for s in info.get("symbols", []):
            if (
                s.get("quoteAsset") == "USDT"
                and s.get("contractType") == "PERPETUAL"
                and s.get("status") == "TRADING"
            ):
                symbols.append(s["symbol"])
        return symbols
    except Exception as e:
        handle_client_error(e)
        return []



def get_filtered_tickers(min_volume=20_000_000):
    try:
        filtered = []
        all_symbols = get_futures_usdt_symbols()
        tickers = client.ticker_24hr_price_change()
        ticker_dict = {t["symbol"]: t for t in tickers}

        for symbol in all_symbols:
            info = ticker_dict.get(symbol)
            quote_volume = float(info["quoteVolume"])
           

            if (min_volume <= quote_volume):
                filtered.append((symbol))

            # Aşırı yüklenmeyi önlemek için kısa bir gecikme önerilir
            time.sleep(0.1)

        filtered.sort(key=lambda x: x[1], reverse=True)  # hacme göre sırala
        return filtered

    except Exception as e:
        handle_client_error(e)
        return []
