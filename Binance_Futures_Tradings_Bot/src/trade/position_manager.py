from binance.error import ClientError
from time import sleep

class PositionManager:
    def __init__(self, client, tp=0.012, sl=0.009, volume=10, leverage=10, margin_type='ISOLATED', max_positions=100):
        """
        Binance Futures client objesini dışarıdan alır ve temel parametreleri ayarlar.

        Args:
            client: Binance Futures API client nesnesi (src/api/binance_client.py içinden).
            tp (float): Kar alma yüzdesi (take profit).
            sl (float): Zarar durdurma yüzdesi (stop loss).
            volume (float): Pozisyon büyüklüğü USDT cinsinden.
            leverage (int): Kaldıraç oranı.
            margin_type (str): Margin tipi ('ISOLATED' veya 'CROSSED').
            max_positions (int): Aynı anda açık pozisyon sayısı limiti.
        """
        self.client = client
        self.tp = tp
        self.sl = sl
        self.volume = volume
        self.leverage = leverage
        self.margin_type = margin_type
        self.max_positions = max_positions

    def get_balance_usdt(self):
        try:
            balances = self.client.balance()
            for bal in balances:
                if bal['asset'] == 'USDT':
                    return float(bal['balance'])
        except ClientError as e:
            print(f"Error getting balance: {e}")
        return None

    def get_open_positions(self):
        try:
            positions = self.client.get_position_risk()
            open_positions = []
            for p in positions:
                if float(p['positionAmt']) != 0:
                    open_positions.append(p['symbol'])
            return open_positions
        except ClientError as e:
            print(f"Error getting positions: {e}")
            return []

    def get_open_orders(self, symbol=None):
        """
        Sembol belirtilirse sadece o sembolün açık emirlerini getirir,
        belirtilmezse tüm açık emirleri getirir.
        """
        try:
            if symbol:
                return self.client.get_open_orders(symbol=symbol)
            else:
                # Eğer API bu şekilde tüm açık emirleri desteklemiyorsa, sembol listesi üzerinden dönebilirsiniz.
                return []
        except ClientError as e:
            print(f"Error getting open orders: {e}")
            return []

    def cancel_order(self, symbol, order_id):
        try:
            return self.client.cancel_order(symbol=symbol, orderId=order_id)
        except ClientError as e:
            print(f"Error cancelling order {order_id} for {symbol}: {e}")

    def cleanup_orders_for_closed_positions(self):
        """
        Pozisyonu olmayan sembollere ait açık emirleri iptal eder.
        Pozisyonu olan sembollerdeki emirleri bırakır.
        """
        open_positions = self.get_open_positions()
        # API'de tüm açık emirleri sembol parametresi olmadan alma desteklenmiyorsa,
        # bu fonksiyon sembol bazında çağrılmalıdır.
        # Burada sembol listesi yerine pozisyon olmayan sembollere ait emirler temizleniyor.

        # Örnek olarak pozisyonu olmayan semboller üzerinde işlem yapabilirsiniz.
        # Burada basitleştirilmiş hali:
        for symbol in self.get_symbols_tracked():
            if symbol not in open_positions:
                open_orders = self.get_open_orders(symbol)
                for order in open_orders:
                    self.cancel_order(symbol, order['orderId'])
                    print(f"Pozisyonu olmayan {symbol} için açık emir iptal edildi: {order['orderId']}")

    def get_symbols_tracked(self):
        """
        İşlem yapılan veya takip edilen sembollerin listesi.
        Geliştirilebilir veya dışarıdan parametre olarak alınabilir.
        """
        # Örnek sabit liste, sizin sisteminize göre dinamik olabilir.
        return ['BTCUSDT', 'ETHUSDT', 'BNBUSDT']

    def get_price_precision(self, symbol):
        try:
            info = self.client.exchange_info()
            for s in info['symbols']:
                if s['symbol'] == symbol:
                    return s['pricePrecision']
        except ClientError as e:
            print(f"Error getting price precision for {symbol}: {e}")
        return 8  # default fallback

    def get_qty_precision(self, symbol):
        try:
            info = self.client.exchange_info()
            for s in info['symbols']:
                if s['symbol'] == symbol:
                    return s['quantityPrecision']
        except ClientError as e:
            print(f"Error getting quantity precision for {symbol}: {e}")
        return 8  # default fallback

    def set_leverage_and_margin(self, symbol):
        try:
            self.client.change_margin_type(symbol=symbol, marginType=self.margin_type)
            self.client.change_leverage(symbol=symbol, leverage=self.leverage)
        except ClientError as e:
            print(f"Error setting leverage/margin for {symbol}: {e}")

    def open_limit_position(self, symbol, side):
        """
        Limit emirle pozisyon açar, ardından stop loss ve take profit emirleri koyar.

        Args:
            symbol (str): İşlem sembolü.
            side (str): 'buy' veya 'sell' yönü.

        Returns:
            bool: Başarılıysa True, hata varsa False döner.
        """
        try:
            price = float(self.client.ticker_price(symbol=symbol))
            qty_precision = self.get_qty_precision(symbol)
            price_precision = self.get_price_precision(symbol)
            qty = round(self.volume / price, qty_precision)
            print(qty_precision)

            # Eğer pozisyon zaten açıksa işlem yapılmaz.
            open_positions = self.get_open_positions()
            if symbol in open_positions:
                print(f"{symbol} için zaten açık pozisyon var.")
                return False

            order_side = 'BUY' if side.lower() == 'buy' else 'SELL'

            print(f"{symbol} için {order_side} yönünde limit emir veriliyor: miktar={qty}, fiyat={price}")
            order = self.client.place_order(
                symbol=symbol,
                side=order_side,
                order_type='LIMIT',
                timeInForce='GTC',
                quantity=qty,
                price=round(price, price_precision)
            )
            print("Pozisyon açma emri gönderildi:", order)
            sleep(2)

            # Kar al ve zarar durdur fiyatları
            if side.lower() == 'buy':
                sl_price = round(price * (1 - self.sl), price_precision)
                tp_price = round(price * (1 + self.tp), price_precision)
                sl_side = 'SELL'
                tp_side = 'SELL'
            else:
                sl_price = round(price * (1 + self.sl), price_precision)
                tp_price = round(price * (1 - self.tp), price_precision)
                sl_side = 'BUY'
                tp_side = 'BUY'

            # Stop Loss emri
            sl_order = self.client.place_order(
                symbol=symbol,
                side=sl_side,
                order_type='STOP_MARKET',
                quantity=qty,
                stopPrice=sl_price,
                timeInForce='GTC'
            )
            print("Stop Loss emri oluşturuldu:", sl_order)
            sleep(1)

            # Take Profit emri
            tp_order = self.client.place_order(
                symbol=symbol,
                side=tp_side,
                order_type='TAKE_PROFIT_MARKET',
                quantity=qty,
                stopPrice=tp_price,
                timeInForce='GTC'
            )
            print("Take Profit emri oluşturuldu:", tp_order)

            return True

        except ClientError as e:
            print(f"Pozisyon açılırken hata: {e}")
            return False

    def reverse_position_if_signal(self, symbol, new_side):
        """
        Eğer açık pozisyon varsa ve gelen sinyal ters yönde ise,
        pozisyon kapatılır, yeni pozisyon açılır.

        Args:
            symbol (str): İşlem sembolü.
            new_side (str): Yeni sinyal yönü ('buy' veya 'sell').

        Returns:
            bool: Başarılıysa True, hata varsa False döner.
        """
        open_positions = self.get_open_positions()
        if symbol not in open_positions:
            print(f"{symbol} için açık pozisyon yok, yeni pozisyon açılıyor.")
            return self.open_limit_position(symbol, new_side)

        pos_info = self.client.get_position_risk()
        current_amt = 0
        for p in pos_info:
            if p['symbol'] == symbol:
                current_amt = float(p['positionAmt'])
                break
        if current_amt == 0:
            print(f"{symbol} pozisyon bilgisi alınamadı veya pozisyon kapalı.")
            return self.open_limit_position(symbol, new_side)

        current_side = 'buy' if current_amt > 0 else 'sell'
        if current_side == new_side.lower():
            print(f"{symbol} için zaten aynı yönde pozisyon var.")
            return False

        try:
            close_side = 'SELL' if current_side == 'buy' else 'BUY'
            qty_precision = self.get_qty_precision(symbol)
            price_precision = self.get_price_precision(symbol)
            qty = round(abs(current_amt), qty_precision)

            print(f"{symbol} için mevcut pozisyon kapatılıyor (yön: {close_side}).")
            close_order = self.client.place_order(
                symbol=symbol,
                side=close_side,
                order_type='MARKET',
                quantity=qty
            )
            print("Pozisyon kapatma emri gönderildi:", close_order)
            sleep(2)

            # Pozisyon kapandığında açık emirler (stop, take profit) temizlenmeli
            self.cleanup_orders_for_closed_positions()

            # Yeni pozisyon açılır
            return self.open_limit_position(symbol, new_side)

        except ClientError as e:
            print(f"{symbol} pozisyonu tersine çevirirken hata: {e}")
            return False
