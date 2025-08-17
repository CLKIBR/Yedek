from time import sleep
from core.client import client, handle_client_error
from core.precision import get_price_precision, get_qty_precision
from config.settings import ORDER_VOLUME, TP_PERCENTAGE, SL_PERCENTAGE

def open_order(symbol: str, side: str):
    try:
        price = float(client.ticker_price(symbol)['price'])
        qty_precision = get_qty_precision(symbol)
        price_precision = get_price_precision(symbol)
        qty = round(ORDER_VOLUME / price, qty_precision)

        order_side = 'BUY' if side == 'buy' else 'SELL'
        exit_side = 'SELL' if side == 'buy' else 'BUY'

        # Limit order açma
        resp1 = client.new_order(
            symbol=symbol,
            side=order_side,
            type='LIMIT',
            quantity=qty,
            timeInForce='GTC',
            price=round(price, price_precision)
        )
        print(f"{symbol} {side.upper()} order placed.")
        print(resp1)
        sleep(2)

        # Stop Loss emri
        sl_price = price - price * SL_PERCENTAGE if side == 'buy' else price + price * SL_PERCENTAGE
        resp2 = client.new_order(
            symbol=symbol,
            side=exit_side,
            type='STOP_MARKET',
            quantity=qty,
            timeInForce='GTC',
            stopPrice=round(sl_price, price_precision)
        )
        print("Stop Loss order placed.")
        print(resp2)
        sleep(2)

        # Take Profit emri
        tp_price = price + price * TP_PERCENTAGE if side == 'buy' else price - price * TP_PERCENTAGE
        resp3 = client.new_order(
            symbol=symbol,
            side=exit_side,
            type='TAKE_PROFIT_MARKET',
            quantity=qty,
            timeInForce='GTC',
            stopPrice=round(tp_price, price_precision)
        )
        print("Take Profit order placed.")
        print(resp3)

    except Exception as error:
        handle_client_error(error)


def close_open_orders(symbol: str):
    try:
        open_orders = client.get_open_orders(symbol=symbol)
        for order in open_orders:
            resp = client.cancel_order(symbol=symbol, orderId=order['orderId'])
            print(f"Order {order['orderId']} for {symbol} cancelled.")
        sleep(1)
    except Exception as error:
        handle_client_error(error)


def set_leverage(symbol: str, leverage: int):
    try:
        response = client.change_leverage(symbol=symbol, leverage=leverage,recvWindow=6000)
        print(f"{symbol} için kaldıraç {leverage} olarak ayarlandı")
        sleep(1)
    except Exception as error:
        handle_client_error(error)


def set_mode(symbol: str, margin_type: str):
    try:
        client.change_margin_type(symbol=symbol, marginType=margin_type,recvWindow=6000)
        print(f"{symbol} için margin tipi {margin_type} olarak ayarlandı.")
        sleep(1)
    except Exception as error:
        if "No need to change margin type" in str(error):
            print(f"{symbol} için margin tipi zaten {margin_type}, değişiklik gerekmedi.")
        else:
            handle_client_error(error)
