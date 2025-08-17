from core.client import client
from typing import List, Dict
from binance.error import ClientError
from data.symbol_utils import get_futures_usdt_symbols

def get_pos() -> List[Dict]:
    try:
        resp = client.get_position_risk()
        pos = []
        for elem in resp:
            if float(elem['positionAmt']) != 0:
                pos.append(elem['symbol'])
        return pos
    except ClientError as e:
        print(f"Pozisyonlar alınırken hata oluştu: {e}")
        return []

def check_orders() -> List[str]:
    try:
        response = client.get_orders(recvWindow=6000)
        sym = []
        for elem in response:
            sym.append(elem['symbol'])
        return sym
    except ClientError as e:
        print(f"Açık emir kontrolü sırasında hata oluştu: {e}")
        return []


def close_open_orders(symbol):
    try:
        response = client.cancel_open_orders(symbol=symbol, recvWindow=6000)
        print(response)
    except ClientError as e:
        print(print(f"Açık emir kapatma sırasında hata oluştu: {e}"))

