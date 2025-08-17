import json
import time
import logging
import yaml

from src.api.binance_client import BinanceClient
from src.trade.position_manager import PositionManager
from src.strategy.example_strategy import ExampleStrategy
from src.utils.logger import setup_logging  # setup_logging fonksiyonunu içeren modülünüz

logger = logging.getLogger("binance_bot")


def load_config(path="config/config.yaml"):
    with open(path, "r", encoding="utf-8") as file:
        return yaml.safe_load(file)


def load_symbols(file_path):
    with open(file_path, "r", encoding="utf-8") as f:
        return json.load(f)


def main():
    config = load_config()
    logger.info("Yapılandırma yüklendi.")

    # Binance Client oluşturma
    api_key = config["binance"]["api_key"]
    api_secret = config["binance"]["api_secret"]
    base_url = config["binance"].get("base_url", "https://testnet.binancefuture.com")

    client = BinanceClient(api_key, api_secret, base_url)
    logger.info("Binance istemcisi oluşturuldu.")

    # PositionManager nesnesini oluşturuyoruz
    position_manager = PositionManager(
    client=client,
    tp=config["trade"].get("take_profit", 0.012),
    sl=config["trade"].get("stop_loss", 0.009),
    volume=config["trade"].get("volume", 10),
    leverage=config["trade"].get("leverage", 10),
    margin_type=config["trade"].get("margin_type", "ISOLATED"),
    max_positions=config["trade"].get("max_positions", 100)
    )
    logger.info("PositionManager oluşturuldu.")

    # Sembolleri JSON dosyasından yükle
    symbols_file = config["trade"]["symbols_file"]
    symbols = load_symbols(symbols_file)
    logger.info(f"Takip edilen semboller: {symbols}")

    # Stratejileri semboller bazında oluştur
    strategies = {}
    for symbol in symbols:
        params = config.get("strategy", {}).get("params", {}).copy()
        params["symbol"] = symbol
        strategies[symbol] = ExampleStrategy(client, params)

    while True:
        try:
            for symbol, strategy in strategies.items():
                signal = strategy.generate_signal()
                action = signal.get('action', '').lower()
                logger.info(f"{symbol} için alınan sinyal: {action}")

                if action in ["buy", "sell"]:
                    # Pozisyonu tersine çevir veya aç
                    result = position_manager.reverse_position_if_signal(symbol, action)
                    if result:
                        logger.info(f"{symbol} için {action} pozisyonu açıldı veya tersine çevrildi.")
                    else:
                        logger.info(f"{symbol} için pozisyon açma/tersine çevirme işlemi yapılmadı.")
                else:
                    logger.info(f"{symbol} için işlem yapılmadı. Sinyal: {action}")

            # Pozisyonu kapanan semboller için gereksiz açık emirleri temizle
            position_manager.cleanup_orders_for_closed_positions()

            interval = config["trade"].get("interval", 60)
            time.sleep(interval)

        except Exception as e:
            logger.error(f"Bir hata oluştu: {e}")
            time.sleep(5)


if __name__ == "__main__":
    setup_logging()  # Logging yapılandırmasını yükle
    main()
