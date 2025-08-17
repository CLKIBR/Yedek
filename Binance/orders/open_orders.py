from time import sleep
from core.precision import get_price_precision, get_qty_precision
from core.client import client
from binance.error import ClientError
from config.settings import LEVERAGE, MARGIN_TYPE, TP_PERCENTAGE, SL_PERCENTAGE
from utils.notifier import Notifier
from datetime import datetime

def open_order(symbol, side, balance):
    order_volume = round(balance, 3) * 5
    price = float(client.ticker_price(symbol)['price'])
    qty_precision = get_qty_precision(symbol)
    price_precision = get_price_precision(symbol)
    qty = round(order_volume / price, qty_precision)
    notifier = Notifier()
    now = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

    is_long = side.lower() == 'buy'
    entry_side = 'BUY' if is_long else 'SELL'
    exit_side = 'SELL' if is_long else 'BUY'
    direction = 'LONG' if is_long else 'SHORT'

    try:
        # Limit emir
        client.new_order(
            symbol=symbol,
            side=entry_side,
            type='LIMIT',
            quantity=qty,
            timeInForce='GTC',
            price=round(price, price_precision)
        )
        print(symbol, direction, "Limit Emri Verildi")

        # Telegram - Limit emri bildirimi
        limit_message = (
            f"📈 Sembol: <b>{symbol}</b>\n"
            f"🔄 Yön: {direction}\n"
            f"⚙️ Emir Tipi: LIMIT\n"
            f"💼 Margin Türü: {MARGIN_TYPE}\n"
            f"📊 Kaldıraç: {LEVERAGE}x\n"
            f"💰 Emir Fiyatı: {round(price, price_precision)}\n"
            f"📦 Miktar: {qty} {symbol.replace('USDT', '')}\n"
            f"💵 Pozisyon Büyüklüğü: {order_volume:.2f} USDT\n"
            f"🕒 Açılış Zamanı: {now}"
        )
        notifier.send_message(limit_message)
        sleep(2)

        # Stop Loss hesaplama ve gönderme
        sl_price = round(price - price * SL_PERCENTAGE, price_precision) if is_long else round(price + price * SL_PERCENTAGE, price_precision)
        client.new_order(
            symbol=symbol,
            side=exit_side,
            type='STOP_MARKET',
            quantity=qty,
            timeInForce='GTC',
            stopPrice=sl_price
        )
        print(symbol, sl_price, "Zarar Durdur (SL) Emri Verildi")

        # Telegram - SL bildirimi
        sl_message = (
            f"🚨 <b>Zarar Durdur Emri</b> Emri\n"
            f"📈 Sembol: <b>{symbol}</b>\n"
            f"🔄 Yön: {direction}\n"
            f"⚙️ Emir Tipi: STOP_MARKET\n"
            f"💼 Margin Türü: {MARGIN_TYPE}\n"
            f"📊 Kaldıraç: {LEVERAGE}x\n"
            f"🛡️ SL Fiyatı: {sl_price}\n"
            f"📦 Miktar: {qty} {symbol.replace('USDT', '')}\n"
            f"💵 Pozisyon Büyüklüğü: {order_volume:.2f} USDT\n"
            f"🕒 Zaman: {now}"
        )
        notifier.send_message(sl_message)

        sleep(2)

        # Take Profit hesaplama ve gönderme
        tp_price = round(price + price * TP_PERCENTAGE, price_precision) if is_long else round(price - price * TP_PERCENTAGE, price_precision)
        client.new_order(
            symbol=symbol,
            side=exit_side,
            type='TAKE_PROFIT_MARKET',
            quantity=qty,
            timeInForce='GTC',
            stopPrice=tp_price
        )
        print(symbol, tp_price, "Kar Al (TP) Emri Verildi")

        # Telegram - TP bildirimi
        tp_message = (
            f"✅ <b>Kâr Al Emri</b> Emri\n"
            f"📈 Sembol: <b>{symbol}</b>\n"
            f"🔄 Yön: {direction}\n"
            f"⚙️ Emir Tipi: TAKE_PROFIT_MARKET\n"
            f"💼 Margin Türü: {MARGIN_TYPE}\n"
            f"📊 Kaldıraç: {LEVERAGE}x\n"
            f"🎯 TP Fiyatı: {tp_price}\n"
            f"📦 Miktar: {qty} {symbol.replace('USDT', '')}\n"
            f"💵 Pozisyon Büyüklüğü: {order_volume:.2f} USDT\n"
            f"🕒 Zaman: {now}"
        )
        notifier.send_message(tp_message)


    except ClientError as error:
        print(
            "Hata oluştu. status: {}, error code: {}, error message: {}".format(
                error.status_code, error.error_code, error.error_message
            )
        )
