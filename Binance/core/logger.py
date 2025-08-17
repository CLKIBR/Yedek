import logging
import sys
from logging.handlers import RotatingFileHandler
import os

LOG_DIR = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "logs")
os.makedirs(LOG_DIR, exist_ok=True)

LOG_FILE = os.path.join(LOG_DIR, "bot.log")

def get_logger(name: str) -> logging.Logger:
    """
    Belirtilen isimle, hem konsola hem dosyaya log yazan Logger objesi döner.
    Dosya boyutu 5 MB'a ulaştığında yeni dosyaya geçer, en fazla 3 yedek dosya tutulur.
    """
    logger = logging.getLogger(name)
    if logger.hasHandlers():
        return logger  # Logger zaten varsa, yeniden ayarlama yapma

    logger.setLevel(logging.DEBUG)

    # Konsola log için handler
    console_handler = logging.StreamHandler(sys.stdout)
    console_handler.setLevel(logging.INFO)  # Konsola sadece INFO ve üzeri seviyeleri yaz

    # Dosyaya log için handler (dönerli dosya)
    file_handler = RotatingFileHandler(
        LOG_FILE, maxBytes=5*1024*1024, backupCount=3, encoding="utf-8"
    )
    file_handler.setLevel(logging.DEBUG)  # Dosyaya DEBUG ve üzeri seviyeler yazılır

    # Log formatı
    formatter = logging.Formatter(
        '[%(asctime)s] [%(levelname)s] [%(name)s] - %(message)s',
        datefmt='%Y-%m-%d %H:%M:%S'
    )
    console_handler.setFormatter(formatter)
    file_handler.setFormatter(formatter)

    # Handlerları logger'a ekle
    logger.addHandler(console_handler)
    logger.addHandler(file_handler)

    return logger
