import unittest
from unittest.mock import MagicMock, patch
from src.execution.order_executor import OrderExecutor


class TestOrderExecutor(unittest.TestCase):

    def setUp(self):
        self.mock_client = MagicMock()
        self.executor = OrderExecutor(self.mock_client)

    def test_execute_order_success(self):
        mock_response = {"orderId": 123, "status": "NEW"}
        self.mock_client.place_order.return_value = mock_response

        order_data = {
            "symbol": "BTCUSDT",
            "side": "BUY",
            "type": "MARKET",
            "quantity": 0.001,
            "price": None
        }

        response = self.executor.execute_order(order_data)

        self.assertEqual(response, mock_response)
        self.mock_client.place_order.assert_called_once_with(
            symbol="BTCUSDT",
            side="BUY",
            order_type="MARKET",
            quantity=0.001,
            price=None
        )

    def test_execute_order_failure(self):
        self.mock_client.place_order.side_effect = Exception("API Hatası")

        order_data = {
            "symbol": "BTCUSDT",
            "side": "SELL",
            "type": "LIMIT",
            "quantity": 0.002,
            "price": 30000
        }

        response = self.executor.execute_order(order_data)

        self.assertIn("error", response)
        self.assertEqual(response["error"], "API Hatası")

    def test_cancel_order_success(self):
        mock_response = {"orderId": 123, "status": "CANCELED"}
        self.mock_client.cancel_order.return_value = mock_response

        response = self.executor.cancel_order("BTCUSDT", 123)

        self.assertEqual(response, mock_response)
        self.mock_client.cancel_order.assert_called_once_with("BTCUSDT", 123)

    def test_cancel_order_failure(self):
        self.mock_client.cancel_order.side_effect = Exception("İptal Hatası")

        response = self.executor.cancel_order("BTCUSDT", 999)

        self.assertIn("error", response)
        self.assertEqual(response["error"], "İptal Hatası")

    def test_get_open_orders_success(self):
        mock_orders = [{"orderId": 1}, {"orderId": 2}]
        self.mock_client.get_open_orders.return_value = mock_orders

        orders = self.executor.get_open_orders("BTCUSDT")

        self.assertEqual(orders, mock_orders)
        self.mock_client.get_open_orders.assert_called_once_with("BTCUSDT")

    def test_get_open_orders_failure(self):
        self.mock_client.get_open_orders.side_effect = Exception("Getirme Hatası")

        orders = self.executor.get_open_orders("BTCUSDT")

        self.assertIn("error", orders)
        self.assertEqual(orders["error"], "Getirme Hatası")


if __name__ == "__main__":
    unittest.main()
