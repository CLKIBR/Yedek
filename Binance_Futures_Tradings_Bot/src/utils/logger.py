import logging
import logging.config
import yaml
from pathlib import Path


def setup_logging(
    default_path: str = "config/logging.yaml",
    default_level: int = logging.INFO,
    env_key: str = "LOG_CFG"
):
    """
    YAML formatındaki logging konfigürasyon dosyasını yükler.
    Dosya bulunamazsa temel loglama ayarı yapılır.
    """
    path = Path(default_path)
    value = None
    import os
    env_path = os.getenv(env_key, None)
    if env_path:
        path = Path(env_path)

    if path.exists():
        with open(path, "rt", encoding="utf-8") as f:
            config = yaml.safe_load(f.read())
        logging.config.dictConfig(config)
    else:
        logging.basicConfig(level=default_level)
        logging.getLogger().warning(f"Log konfigürasyon dosyası bulunamadı: {path}")


logger = logging.getLogger("binance_bot")
