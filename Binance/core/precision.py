from core.client import client, handle_client_error

# Sembolün fiyat hassasiyetini getirir (örneğin BTCUSDT için 1, XRPUSDT için 4)
def get_price_precision(symbol):
    try:
        resp = client.exchange_info()['symbols']
        for elem in resp:
            if elem['symbol'] == symbol:
                return elem['pricePrecision']
    except Exception as error:
        handle_client_error(error)

# Sembolün miktar hassasiyetini getirir (örneğin BTCUSDT için 3, XRPUSDT için 1)
def get_qty_precision(symbol):
    try:
        resp = client.exchange_info()['symbols']
        for elem in resp:
            if elem['symbol'] == symbol:
                return elem['quantityPrecision']
    except Exception as error:
        handle_client_error(error)
