import unittest
from unittest.mock import patch, MagicMock
from src.api.binance_client import BinanceClient


class TestBinanceClient(unittest.TestCase):

    def setUp(self):
        self.api_key = "test_api_key"
        self.api_secret = "test_api_secret"
        self.client = BinanceClient(self.api_key, self.api_secret)

    @patch("src.api.binance_client.requests.post")
    def test_place_order_success(self, mock_post):
        mock_response = MagicMock()
        mock_response.status_code = 200
        mock_response.json.return_value = {"orderId": 12345, "status": "NEW"}

        mock_post.return_value = mock_response

        response = self.client.place_order(
            symbol="BTCUSDT",
            side="BUY",
            order_type="MARKET",
            quantity=0.001,
            price=None
        )

        self.assertEqual(response.get("orderId"), 12345)
        self.assertEqual(response.get("status"), "NEW")
        mock_post.assert_called_once()

    @patch("src.api.binance_client.requests.post")
    def test_place_order_failure(self, mock_post):
        mock_response = MagicMock()
        mock_response.status_code = 400
        mock_response.text = "Bad Request"
        mock_response.raise_for_status.side_effect = Exception("Bad Request")

        mock_post.return_value = mock_response

        with self.assertRaises(Exception):
            self.client.place_order(
                symbol="BTCUSDT",
                side="BUY",
                order_type="MARKET",
                quantity=0,
                price=None
            )

    @patch("src.api.binance_client.requests.get")
    def test_get_account_info(self, mock_get):
        mock_response = MagicMock()
        mock_response.status_code = 200
        mock_response.json.return_value = {"balances": []}

        mock_get.return_value = mock_response

        response = self.client.get_account_info()

        self.assertIn("balances", response)
        mock_get.assert_called_once()

if __name__ == "__main__":
    unittest.main()
