from datetime import datetime, timezone
import time


def current_milli_time() -> int:
    """
    Şu anki zamanı milisaniye cinsinden döner.
    """
    return int(time.time() * 1000)


def utc_now_iso() -> str:
    """
    UTC zamanını ISO 8601 formatında döner.
    """
    return datetime.utcnow().replace(tzinfo=timezone.utc).isoformat()


def safe_float(value, default=0.0) -> float:
    """
    Güvenli float dönüşümü yapar. Başarısız olursa default döner.
    """
    try:
        return float(value)
    except (TypeError, ValueError):
        return default
