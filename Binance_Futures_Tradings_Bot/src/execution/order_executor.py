import logging
from typing import Dict, Any
from src.api.binance_client import BinanceClient

logger = logging.getLogger("binance_bot")


class OrderExecutor:
    """
    Emirlerin Binance Futures üzerinde gönderilmesi, iptali ve yönetimi.
    """

    def __init__(self, client: BinanceClient):
        self.client = client

    def execute_order(self, order_data: Dict[str, Any]) -> Dict[str, Any]:
        """
        Emir gönderir.
        :param order_data: {
            "symbol": str,
            "side": "BUY" veya "SELL",
            "type": "MARKET" veya "LIMIT",
            "quantity": float,
            "price": float (opsiyonel, LIMIT için)
        }
        :return: API cevabı
        """
        symbol = order_data.get("symbol")
        side = order_data.get("side")
        order_type = order_data.get("type", "MARKET")
        quantity = order_data.get("quantity")
        price = order_data.get("price", None)

        try:
            logger.info(f"Emir gönderiliyor: {side} {quantity} {symbol} @ {price if price else 'MARKET'}")
            response = self.client.place_order(
                symbol=symbol,
                side=side,
                order_type=order_type,
                quantity=quantity,
                price=price
            )
            logger.info(f"Emir başarıyla gönderildi. Order ID: {response.get('orderId')}")
            return response
        except Exception as e:
            logger.error(f"Emir gönderilemedi: {e}")
            return {"error": str(e)}

    def cancel_order(self, symbol: str, order_id: int) -> Dict[str, Any]:
        """
        Verilen emir ID'sini iptal eder.
        """
        try:
            logger.info(f"Emir iptal ediliyor: {symbol} Order ID: {order_id}")
            response = self.client.cancel_order(symbol, order_id)
            logger.info("Emir iptal edildi.")
            return response
        except Exception as e:
            logger.error(f"Emir iptal edilemedi: {e}")
            return {"error": str(e)}

    def get_open_orders(self, symbol: str) -> Any:
        """
        Açık emir listesini döner.
        """
        try:
            orders = self.client.get_open_orders(symbol)
            return orders
        except Exception as e:
            logger.error(f"Açık emirler alınamadı: {e}")
            return {"error": str(e)}
