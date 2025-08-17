import time
import logging

class Timer:
    def __init__(self):
        self.start_time = None
        self.elapsed = 0.0
        self.logger = logging.getLogger(__name__)

    def start(self):
        """Zamanlayıcıyı başlatır."""
        self.start_time = time.perf_counter()
        self.logger.debug("Timer başlatıldı.")

    def stop(self):
        """Zamanlayıcıyı durdurur ve geçen süreyi döner."""
        if self.start_time is None:
            self.logger.warning("Timer daha önce başlatılmamış.")
            return 0.0
        self.elapsed = time.perf_counter() - self.start_time
        self.start_time = None
        self.logger.debug(f"Timer durduruldu. Geçen süre: {self.elapsed:.4f} saniye.")
        return self.elapsed

    def elapsed_time(self):
        """Zamanlayıcı başlatıldıysa geçen süreyi döner, değilse 0 döner."""
        if self.start_time is None:
            return 0.0
        return time.perf_counter() - self.start_time

    def sleep_until(self, target_time):
        """
        Belirtilen hedef zamana kadar bekler.
        :param target_time: float, epoch formatında hedef zaman
        """
        now = time.time()
        wait_time = target_time - now
        if wait_time > 0:
            self.logger.debug(f"{wait_time:.4f} saniye bekleniyor...")
            time.sleep(wait_time)
