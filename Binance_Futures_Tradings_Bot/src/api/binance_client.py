from binance.um_futures import UMFutures
from binance.error import ClientError
import logging

logger = logging.getLogger("binance_bot")


class BinanceClient:
    def __init__(self, api_key: str, api_secret: str, testnet: bool = True):
        base_url = "https://testnet.binancefuture.com" if testnet else "https://fapi.binance.com"
        self.client = UMFutures(key=api_key, secret=api_secret, base_url=base_url)

    def get_account_info(self):
        try:
            return self.client.account()
        except ClientError as e:
            logger.error(f"Hesap bilgisi alınamadı: {e}")
            return {}

    def get_position(self, symbol: str):
        try:
            positions = self.client.get_position_risk()
            for pos in positions:
                if pos["symbol"] == symbol:
                    return pos
        except ClientError as e:
            logger.error(f"Pozisyon alınamadı ({symbol}): {e}")
        return {}

    def place_order(self, symbol: str, side: str, order_type: str, quantity: float, price: float = None, reduce_only: bool = False):
        try:
            params = {
                "symbol": symbol,
                "side": side.upper(),
                "type": order_type.upper(),
                "quantity": quantity,
                "reduceOnly": reduce_only
            }
            if order_type.upper() == "LIMIT" and price:
                params["price"] = price
                params["timeInForce"] = "GTC"

            return self.client.new_order(**params)
        except ClientError as e:
            logger.error(f"Emir gönderilemedi: {e}")
            return {}

    def cancel_order(self, symbol: str, order_id: int):
        try:
            return self.client.cancel_order(symbol=symbol, orderId=order_id)
        except ClientError as e:
            logger.error(f"Emir iptal edilemedi ({order_id}): {e}")
            return {}

    def get_open_orders(self, symbol: str):
        try:
            return self.client.get_open_orders(symbol=symbol)
        except ClientError as e:
            logger.error(f"Açık emirler alınamadı ({symbol}): {e}")
            return []

    def get_klines(self, symbol: str, interval: str = "1m", limit: int = 500):
        try:
            return self.client.klines(symbol=symbol, interval=interval, limit=limit)
        except ClientError as e:
            logger.error(f"Kline verisi alınamadı ({symbol}): {e}")
            return []

    def ticker_price(self, symbol: str) -> float:
        try:
            ticker = self.client.ticker_price(symbol=symbol)
            return float(ticker["price"])
        except ClientError as e:
            logger.error(f"Fiyat alınamadı ({symbol}): {e}")
            return 0.0

    def exchange_info(self):
        try:
            return self.client.exchange_info()
        except ClientError as e:
            logger.error(f"Exchange info alınamadı: {e}")
            return {}

    def get_qty_precision(self, symbol: str):
        try:
            info = self.exchange_info()
            for s in info["symbols"]:
                if s["symbol"] == symbol:
                    return s["quantityPrecision"]
        except Exception as e:
            logger.error(f"Miktar hassasiyeti alınamadı: {e}")
        return 3

    def get_price_precision(self, symbol: str):
        try:
            info = self.exchange_info()
            for s in info["symbols"]:
                if s["symbol"] == symbol:
                    return s["pricePrecision"]
        except Exception as e:
            logger.error(f"Fiyat hassasiyeti alınamadı: {e}")
        return 2
