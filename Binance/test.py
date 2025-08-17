from binance.um_futures import UMFutures
from binance.error import ClientError
from config.settings import API_KEY,API_SECRET, LIVE_API_KEY, LIVE_API_SECRET

client = UMFutures(key=LIVE_API_KEY, secret=LIVE_API_SECRET)

try:
    info = client.account()
    print("✅ API bağlantısı başarılı:", info)
except ClientError as e:
    print(f"❌ API hatası - Status: {e.status_code}, Kod: {e.error_code}, Mesaj: {e.error_message}")
