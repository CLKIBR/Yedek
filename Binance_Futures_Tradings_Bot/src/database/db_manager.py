import sqlite3
import threading
import logging
from typing import Optional, Any, List, Tuple

logger = logging.getLogger("binance_bot")


class DBManager:
    """
    SQLite tabanlı basit veritabanı yönetimi.
    Thread-safe bağlantı ve temel CRUD işlemleri sağlar.
    """

    _instance = None
    _lock = threading.Lock()

    def __new__(cls, db_path: str = "binance_bot.db"):
        with cls._lock:
            if cls._instance is None:
                cls._instance = super(DBManager, cls).__new__(cls)
                cls._instance._initialized = False
        return cls._instance

    def __init__(self, db_path: str = "binance_bot.db"):
        if self._initialized:
            return
        self.db_path = db_path
        self.connection = sqlite3.connect(self.db_path, check_same_thread=False)
        self.connection.execute("PRAGMA foreign_keys = ON;")
        self.connection.commit()
        self._initialized = True
        logger.info(f"Veritabanı bağlantısı açıldı: {self.db_path}")

    def execute(self, query: str, params: Optional[Tuple] = None) -> sqlite3.Cursor:
        """
        SQL sorgusu çalıştırır.
        """
        cursor = self.connection.cursor()
        try:
            if params:
                cursor.execute(query, params)
            else:
                cursor.execute(query)
            self.connection.commit()
            return cursor
        except sqlite3.Error as e:
            logger.error(f"Veritabanı hatası: {e} | Sorgu: {query}")
            raise

    def fetchall(self, query: str, params: Optional[Tuple] = None) -> List[Tuple]:
        """
        Çoklu satır sorgusu döner.
        """
        cursor = self.execute(query, params)
        return cursor.fetchall()

    def fetchone(self, query: str, params: Optional[Tuple] = None) -> Optional[Tuple]:
        """
        Tek satır sorgusu döner.
        """
        cursor = self.execute(query, params)
        return cursor.fetchone()

    def close(self):
        """
        Veritabanı bağlantısını kapatır.
        """
        if self.connection:
            self.connection.close()
            logger.info("Veritabanı bağlantısı kapatıldı.")
