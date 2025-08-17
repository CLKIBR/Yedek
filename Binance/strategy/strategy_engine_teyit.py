from data.klines import klines
import ta.volatility
import ta.momentum


class StrategyState:
    IDLE = "IDLE"
    WAITING_CONFIRMATION_LOWER = "WAITING_CONFIRMATION_LOWER"
    WATCHING_RETEST_LOWER = "WATCHING_RETEST_LOWER"
    WAITING_CONFIRMATION_UPPER = "WAITING_CONFIRMATION_UPPER"
    WATCHING_RETEST_UPPER = "WATCHING_RETEST_UPPER"


class RetestStrategy:
    def __init__(self, max_wait_candles=3, break_threshold=0.001):
        """
        :param max_wait_candles: Maksimum bekleme mum sayısı.
        :param break_threshold: Destek/direnç seviyesinin % kaç altında/üstünde kırılma sayılacağı (örneğin 0.001 = %0.1)
        """
        self.state = StrategyState.IDLE
        self.counter = 0
        self.max_wait = max_wait_candles
        self.break_threshold = break_threshold

        self.signal_support = None  # Alt bant için sinyal mumunun open fiyatı (destek)
        self.signal_resistance = None  # Üst bant için sinyal mumunun open fiyatı (direnç)

    def analyze(self, symbol: str) -> str:
        df = klines(symbol)
        if df is None or df.empty or len(df) < 50:
            return "NO_SIGNAL"
        print("Teyit çalıştı")
        close_now = df['Close'].iloc[-1]
        open_now = df['Open'].iloc[-1]

        kc = ta.volatility.KeltnerChannel(df['High'], df['Low'], df['Close'], 20, 10, multiplier=3, original_version=False)
        lower_band = kc.keltner_channel_lband().iloc[-1]
        upper_band = kc.keltner_channel_hband().iloc[-1]

        stoch_rsi_indicator = ta.momentum.StochRSIIndicator(df['Close'], 14, 3, 3)
        stoch_k = stoch_rsi_indicator.stochrsi_k().iloc[-1] * 100
        stoch_d = stoch_rsi_indicator.stochrsi_d().iloc[-1] * 100

        # --- ALT BANT İÇİN SÜREÇ ---
        if self.state == StrategyState.IDLE:
            if close_now < lower_band and stoch_k < 20 and stoch_d < 20:
                self.state = StrategyState.WAITING_CONFIRMATION_LOWER
                self.counter = 0
                self.signal_support = open_now
                return "IZLENIYOR - ALT BAND ALTI TEMAS"

            if close_now > upper_band and stoch_k > 80 and stoch_d > 80:
                self.state = StrategyState.WAITING_CONFIRMATION_UPPER
                self.counter = 0
                self.signal_resistance = open_now
                return "IZLENIYOR - ÜST BAND ÜSTÜ TEMAS"

        elif self.state == StrategyState.WAITING_CONFIRMATION_LOWER:
            self.counter += 1
            if close_now > lower_band:
                self.state = StrategyState.WATCHING_RETEST_LOWER
                self.counter = 0
                return "RETEST BEKLENIYOR - ALT BAND"
            elif self.counter > self.max_wait:
                self._reset()
                return "SINYAL ZAMANI DOLDU - RESET ALT BAND"

        elif self.state == StrategyState.WATCHING_RETEST_LOWER:
            self.counter += 1
            # Kesin destek kırılması kontrolü (close fiyatı, destek seviyesinin break_threshold kadar altında olmalı)
            if close_now < self.signal_support * (1 - self.break_threshold):
                self._reset()
                return "SHORT ALT BAND"
            # Destek seviyesinin üstünde ve Stoch RSI teyidi ile long aç
            elif close_now > self.signal_support and stoch_k > 20 and stoch_d > 20:
                self._reset()
                return "LONG ALT BAND"
            elif self.counter > self.max_wait:
                self._reset()
                return "RETEST ZAMANI DOLDU - RESET ALT BAND"

        # --- ÜST BANT İÇİN SÜREÇ ---
        elif self.state == StrategyState.WAITING_CONFIRMATION_UPPER:
            self.counter += 1
            if close_now < upper_band:
                self.state = StrategyState.WATCHING_RETEST_UPPER
                self.counter = 0
                return "RETEST BEKLENIYOR - ÜST BAND"
            elif self.counter > self.max_wait:
                self._reset()
                return "SINYAL ZAMANI DOLDU - RESET ÜST BAND"

        elif self.state == StrategyState.WATCHING_RETEST_UPPER:
            self.counter += 1
            # Kesin direnç kırılması kontrolü (close fiyatı, direnç seviyesinin break_threshold kadar üstünde olmalı)
            if close_now > self.signal_resistance * (1 + self.break_threshold):
                self._reset()
                return "LONG ÜST BAND"
            # Direnç altında ve Stoch RSI teyidi ile short aç
            elif close_now < self.signal_resistance and stoch_k < 80 and stoch_d < 80:
                self._reset()
                return "SHORT ÜST BAND"
            elif self.counter > self.max_wait:
                self._reset()
                return "RETEST ZAMANI DOLDU - RESET ÜST BAND"

        return "NO_SIGNAL"

    def _reset(self):
        self.state = StrategyState.IDLE
        self.counter = 0
        self.signal_support = None
        self.signal_resistance = None
