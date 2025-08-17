import logging
from typing import Dict, Any

logger = logging.getLogger("binance_bot")


class RiskManager:
    """
    Risk yönetimi sınıfı.
    İşlem sinyallerini değerlendirir ve risk limitlerine uygunluk kontrolü yapar.
    """

    def __init__(self, config: Dict[str, Any]):
        """
        :param config: Yapılandırma sözlüğü (örn. risk limitleri, maksimum pozisyon büyüklüğü)
        """
        risk_cfg = config.get("risk", {})
        self.max_position_size = risk_cfg.get("max_position_size", 0.05)  # Örnek: 5% hesap büyüklüğü
        self.max_loss_per_trade = risk_cfg.get("max_loss_per_trade", 0.01)  # Örnek: %1 zarar limiti
        self.allowed_symbols = risk_cfg.get("allowed_symbols", [])

    def is_risk_acceptable(self, signal_data: Dict[str, Any]) -> bool:
        """
        İşlem sinyaline göre risk değerlendirmesi yapar.
        :param signal_data: Sinyal sözlüğü (örn. action, quantity)
        :return: İşlem yapılabilir ise True, aksi halde False
        """
        action = signal_data.get("action", "HOLD")
        quantity = signal_data.get("quantity", 0)

        if action not in {"BUY", "SELL"}:
            logger.debug("RiskManager: İşlem sinyali HOLD, risk kontrolü pas geçildi.")
            return False

        if quantity <= 0:
            logger.warning("RiskManager: İşlem miktarı 0 veya negatif, risk reddedildi.")
            return False

        # Burada hesap büyüklüğü ve pozisyon büyüklüğü gibi gerçek verilerle kontrol yapılmalı.
        # Örneğin: max_position_size kontrolü, kayıp limiti kontrolü vb.

        logger.debug(f"RiskManager: İşlem için risk uygun bulundu. Action: {action}, Quantity: {quantity}")
        return True
