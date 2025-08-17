from abc import ABC, abstractmethod
from typing import Dict, Any


class StrategyBase(ABC):
    """
    Tüm stratejiler için temel soyut sınıf.
    """
    def __init__(self, client, params: Dict[str, Any]):
        """
        :param client: BinanceClient örneği
        :param params: Strateji parametreleri
        """
        self.client = client
        self.params = params

    @abstractmethod
    def generate_signal(self) -> Dict[str, Any]:
        """
        Stratejiye göre işlem sinyali üretir.
        Dönüş formatı:
        {
            "action": "BUY" / "SELL" / "HOLD",
            "quantity": float,
            "price": float (opsiyonel),
            "reason": str (açıklama)
        }
        """
        pass
