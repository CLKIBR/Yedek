from binance.um_futures import UMFutures
from binance.error import ClientError
from config.settings import API_KEY,API_SECRET, LIVE_API_KEY, LIVE_API_SECRET

# Binance UM Futures istemcisi oluşturuluyor
#client = UMFutures(key=API_KEY, secret=API_SECRET,base_url="https://testnet.binancefuture.com")
client = UMFutures(key=LIVE_API_KEY, secret=LIVE_API_SECRET)

def handle_client_error(error: ClientError):
    print(
        f"Found error. status: {error.status_code}, "
        f"error code: {error.error_code}, "
        f"error message: {error.error_message}"
    )
