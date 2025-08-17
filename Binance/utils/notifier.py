import logging
import requests
from config.settings import TELEGRAM_BOT_TOKEN,TELEGRAM_CHAT_ID

class Notifier:
    def __init__(self, telegram_token=TELEGRAM_BOT_TOKEN, telegram_chat_id=TELEGRAM_CHAT_ID):
        self.logger = logging.getLogger(__name__)
        self.telegram_token = telegram_token
        self.telegram_chat_id = telegram_chat_id

    def send_telegram_message(self, message: str):
        """
        Telegram üzerinden mesaj gönderir.
        :param message: Gönderilecek metin
        """
        if not self.telegram_token or not self.telegram_chat_id:
            self.logger.warning("Telegram token veya chat_id eksik. Mesaj gönderilemiyor.")
            return False

        url = f"https://api.telegram.org/bot{self.telegram_token}/sendMessage"
        payload = {
            "chat_id": self.telegram_chat_id,
            "text": message,
            "parse_mode": "HTML"
        }

        try:
            response = requests.post(url, json=payload, timeout=10)
            response.raise_for_status()
            self.logger.info("Telegram mesajı gönderildi.")
            return True
        except requests.RequestException as e:
            self.logger.error(f"Telegram mesajı gönderilemedi: {e}")
            return False

    def send_message(self, message: str):
        """
        Genel bildirim metodu. Şu anda sadece Telegram destekleniyor.
        İleride Discord, Slack vb. entegrasyonları eklenebilir.
        """
        # Şu an sadece Telegram'a gönderim yapılır
        return self.send_telegram_message(message)
