import pandas as pd
from core.client import client, handle_client_error

# Belirtilen sembol için 15 dakikalık mum verisini DataFrame olarak getirir
def klines(symbol: str , interval = "15m" , limit = 100) -> pd.DataFrame:
    try:
        resp = pd.DataFrame(client.klines(symbol, interval, limit))
        resp = resp.iloc[:,:6]
        resp.columns = ['Time', 'Open', 'High', 'Low', 'Close', 'Volume']
        resp = resp.set_index('Time')
        resp.index = pd.to_datetime(resp.index, unit = 'ms')
        resp = resp.astype(float)
        return resp
    except Exception as error:
        handle_client_error(error)
        return pd.DataFrame()  # Boş DataFrame döner
