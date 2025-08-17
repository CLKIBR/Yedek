import unittest
from unittest.mock import MagicMock
import pandas as pd
from src.strategy.example_strategy import ExampleStrategy


class TestExampleStrategy(unittest.TestCase):

    def setUp(self):
        self.mock_client = MagicMock()
        self.params = {
            "short_ma_period": 7,
            "long_ma_period": 25,
            "rsi_period": 14,
            "rsi_overbought": 70,
            "rsi_oversold": 30,
            "symbol": "BTCUSDT",
            "interval": "1m",
            "quantity": 0.001,
        }
        self.strategy = ExampleStrategy(self.mock_client, self.params)

    def test_generate_signal_buy(self):
        # Kapanış fiyatlarını test için hazırla (RSI oversold, Golden Cross simülasyonu)
        prices = [10] * 40
        for i in range(25, 32):
            prices[i] = 11  # Kısa MA yükselişi simülasyonu

        klines = [[None]*5 for _ in range(40)]
        for i in range(40):
            klines[i][4] = prices[i]

        self.mock_client.get_klines.return_value = klines

        signal = self.strategy.generate_signal()
        self.assertEqual(signal["action"], "BUY")

    def test_generate_signal_hold(self):
        prices = [10] * 40
        klines = [[None]*5 for _ in range(40)]
        for i in range(40):
            klines[i][4] = prices[i]

        self.mock_client.get_klines.return_value = klines

        signal = self.strategy.generate_signal()
        self.assertEqual(signal["action"], "HOLD")

    def test_generate_signal_sell(self):
        # Kısa MA düşüşü, RSI overbought simülasyonu
        prices = [11] * 40
        for i in range(25, 32):
            prices[i] = 10

        klines = [[None]*5 for _ in range(40)]
        for i in range(40):
            klines[i][4] = prices[i]

        self.mock_client.get_klines.return_value = klines

        signal = self.strategy.generate_signal()
        self.assertEqual(signal["action"], "SELL")


if __name__ == "__main__":
    unittest.main()
