import requests
import time
import hmac
import hashlib
from urllib.parse import urlencode

class BinanceClient:
    """
    Binance Futures REST API istemcisi.

    Temel işlevler:
    - İmzalı istekler oluşturma
    - Piyasa verisi alma
    - Emir gönderme ve iptal etme
    """

    BASE_URL = "https://fapi.binance.com"
    TESTNET_URL = "https://testnet.binancefuture.com"

    def __init__(self, api_key: str, api_secret: str, testnet: bool = False):
        self.api_key = api_key
        self.api_secret = api_secret.encode("utf-8")
        self.base_url = self.TESTNET_URL if testnet else self.BASE_URL
        self.session = requests.Session()
        self.session.headers.update({"X-MBX-APIKEY": self.api_key})

    def _get_timestamp(self) -> int:
        return int(time.time() * 1000)

    def _sign_payload(self, params: dict) -> dict:
        """
        Parametreleri imzalar ve 'signature' parametresini ekler.
        """
        query_string = urlencode(params)
        signature = hmac.new(self.api_secret, query_string.encode("utf-8"), hashlib.sha256).hexdigest()
        params["signature"] = signature
        return params

    def _send_request(self, method: str, path: str, params: dict = None, signed: bool = False) -> dict:
        url = self.base_url + path
        params = params or {}

        if signed:
            params["timestamp"] = self._get_timestamp()
            params = self._sign_payload(params)

        try:
            if method == "GET":
                response = self.session.get(url, params=params, timeout=10)
            elif method == "POST":
                response = self.session.post(url, params=params, timeout=10)
            elif method == "DELETE":
                response = self.session.delete(url, params=params, timeout=10)
            else:
                raise ValueError(f"Desteklenmeyen HTTP metodu: {method}")

            response.raise_for_status()
            return response.json()

        except requests.RequestException as e:
            raise RuntimeError(f"API isteği başarısız oldu: {e}")

    # Örnek API metodu: Mevcut pozisyonları al
    def get_position(self, symbol: str) -> dict:
        path = "/fapi/v2/positionRisk"
        params = {"symbol": symbol}
        return self._send_request("GET", path, params=params, signed=True)

    # Örnek API metodu: Emir gönder
    def create_order(self, symbol: str, side: str, order_type: str, quantity: float, price: float = None, time_in_force: str = "GTC") -> dict:
        """
        Emir gönderir.

        Args:
            symbol (str): İşlem çifti, örn. "BTCUSDT"
            side (str): "BUY" veya "SELL"
            order_type (str): "LIMIT" veya "MARKET"
            quantity (float): Miktar
            price (float, optional): Limit fiyatı (limit emir için zorunlu)
            time_in_force (str): Emir geçerlilik süresi, varsayılan "GTC"

        Returns:
            dict: API cevabı
        """
        path = "/fapi/v1/order"
        params = {
            "symbol": symbol,
            "side": side,
            "type": order_type,
            "quantity": quantity,
        }
        if order_type == "LIMIT":
            if price is None:
                raise ValueError("Limit emirlerde fiyat belirtilmelidir.")
            params["price"] = price
            params["timeInForce"] = time_in_force

        return self._send_request("POST", path, params=params, signed=True)

    # Örnek API metodu: Emir iptal et
    def cancel_order(self, symbol: str, order_id: int) -> dict:
        path = "/fapi/v1/order"
        params = {
            "symbol": symbol,
            "orderId": order_id
        }
        return self._send_request("DELETE", path, params=params, signed=True)
