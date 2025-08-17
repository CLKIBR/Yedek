import os
from dotenv import load_dotenv


load_dotenv()
# Binance API anahtarları (Güvenlik için ortam değişkenlerinden okunması önerilir)
API_KEY = os.getenv("BINANCE_API_KEY")
API_SECRET = os.getenv("BINANCE_API_SECRET")
LIVE_API_KEY = os.getenv("BINANCE_LIVE_API_KEY")
LIVE_API_SECRET = os.getenv("BINANCE_LIVE_API_SECRET")



# Genel işlem ayarları
TP_PERCENTAGE = 0.002  # %2 Kar Al
SL_PERCENTAGE = 0.005  # %1 Zarar Kes
ORDER_VOLUME = 100     # İşlem hacmi (leverage ile çarpılır)
LEVERAGE = 75        # Kaldıraç
MARGIN_TYPE = 'ISOLATED'  # 'ISOLATED' veya 'CROSS'
MAX_CONCURRENT_POSITIONS = 1  # Aynı anda açık pozisyon sayısı

# Bildirim ayarları (Telegram örneği)
TELEGRAM_BOT_TOKEN = os.getenv("TELEGRAM_BOT_TOKEN")
TELEGRAM_CHAT_ID = os.getenv("TELEGRAM_CHAT_ID")

# Log dosyası yolu (isteğe bağlı)
LOG_FILE_PATH = "logs/bot.log"
