from core.balance import get_balance_usdt
from core.trade_utils import set_mode, set_leverage
from orders.position_handler import get_pos, check_orders, close_open_orders
from orders.open_orders import open_order
from core.logger import get_logger
from data.symbol_utils import get_filtered_tickers
from strategy.strategy_engine import Super_Trend_Strategy
from config.settings import LEVERAGE, MARGIN_TYPE, MAX_CONCURRENT_POSITIONS
from utils.notifier import Notifier
from time import sleep
from datetime import datetime

def main():
    logger = get_logger(__name__)
    notifier = Notifier()

    #logger.info("Bot başlatıldı.")
    notifier.send_message("🚀 <b>Binance Futures Bot Başlatıldı</b>")

    #symbols = get_filtered_tickers()
    symbols = ["DYDXUSDT"]

    while True:
        balance = get_balance_usdt()
        sleep(1)

        pos = get_pos()        

        print(f"Filitrelenmiş Pariteler: {len(symbols)} => {symbols}")

        if balance is None:
            msg = 'API bağlantısı başarısız. Kontrol edin.'
            logger.warning(msg)
            notifier.send_message(f"⚠️ {msg}")
            continue

        ord = check_orders()

        for s in ord:
            if s not in pos:
                close_open_orders(s)

        if len(pos) < MAX_CONCURRENT_POSITIONS:
            for symbol in symbols:
                excluded_symbols = ['USDCUSDT', 'ETHUSDT', 'MYXUSDT', 'PAXGUSDT', 'TRXUSDT','BTCUSDT','NXPCUSDT']
                if symbol in excluded_symbols:
                    continue

                signal = Super_Trend_Strategy.generate_signal(symbol)

                if signal in ['LONG', 'SHORT'] and symbol not in pos and symbol not in ord:
                    # Emir hazırlığı
                    set_mode(symbol, MARGIN_TYPE)
                    sleep(1)
                    set_leverage(symbol, LEVERAGE)
                    sleep(1)
                    order_side = 'buy' if signal == 'LONG' else 'sell'
                    open_order(symbol, order_side, balance)
                    sleep(1)

                    # Güncel bilgiler
                    now = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
                    new_balance = get_balance_usdt()
                    price_info = f"Leverage: {LEVERAGE}x | Margin: {MARGIN_TYPE}"
                    log_message = (
                        f"{now} | {signal} işlemi gerçekleştirildi. | "
                        f"Sembol: {symbol} | "
                        f"Yön: {'BUY' if order_side == 'buy' else 'SELL'} | "
                        f"{price_info} | "
                        f"Balance: {new_balance:.2f} USDT"
                    )
                    logger.info(log_message)

                    

                    # Pozisyonları güncelle
                    sleep(10)
                    pos = get_pos()
                    ord = check_orders()
                    break
                print("-------------------------------------------------------------------------------------------------------------")

        sleep(60)

if __name__ == '__main__':
    main()
