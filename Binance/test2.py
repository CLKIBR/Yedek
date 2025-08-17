import yfinance as yf
import pandas as pd
import ta
import numpy as np
import matplotlib.pyplot as plt

def safe_series(data, index):
    """Gelen veriyi 1D Series'e güvenli çevirir"""
    if hasattr(data, 'values'):
        arr = data.values
    else:
        arr = np.asarray(data)
    if arr.ndim > 1:
        arr = arr.flatten()
    return pd.Series(arr, index=index)

def get_data(symbol="BTCUSDT", interval="1h", period="1y"):
    df = yf.download(symbol, interval=interval, period=period, auto_adjust=False)
    return df.dropna()

def get_data_15m(symbol="BTCUSDT", period="60d"):
    df = yf.download(symbol, interval="15m", period=period, auto_adjust=False)
    return df.dropna()

def compute_all_indicators(df):
    # 1 saatlik veri
    close = df['Close'].squeeze()
    high = df['High'].squeeze()
    low = df['Low'].squeeze()
    volume = df['Volume'].squeeze()

    df_out = pd.DataFrame(index=df.index)
    df_out['Close'] = close

    # EMA20, EMA50
    df_out['ema20'] = close.ewm(span=20, adjust=False).mean()
    df_out['ema50'] = close.ewm(span=50, adjust=False).mean()

    # ADX, +DI, -DI
    adx_ind = ta.trend.ADXIndicator(high, low, close)
    df_out['adx'] = safe_series(adx_ind.adx(), df.index)
    df_out['plus_di'] = safe_series(adx_ind.adx_pos(), df.index)
    df_out['minus_di'] = safe_series(adx_ind.adx_neg(), df.index)

    # STC
    stc_ind = ta.trend.STCIndicator(close)
    df_out['stc'] = safe_series(stc_ind.stc(), df.index)

    # PSAR
    psar_ind = ta.trend.PSARIndicator(high, low, close)
    df_out['psar'] = safe_series(psar_ind.psar(), df.index)

    # TRIX
    trix_ind = ta.trend.TRIXIndicator(close)
    df_out['trix'] = safe_series(trix_ind.trix(), df.index)

    # Stoch RSI (K ve D)
    stochrsi_ind = ta.momentum.StochRSIIndicator(close)
    df_out['stoch_rsi_k'] = safe_series(stochrsi_ind.stochrsi_k(), df.index)
    df_out['stoch_rsi_d'] = safe_series(stochrsi_ind.stochrsi_d(), df.index)

    # Keltner Channel
    kc = ta.volatility.KeltnerChannel(high, low, close)
    df_out['kc_upper'] = safe_series(kc.keltner_channel_hband(), df.index)
    df_out['kc_lower'] = safe_series(kc.keltner_channel_lband(), df.index)

    # OBV
    obv_ind = ta.volume.OnBalanceVolumeIndicator(close, volume)
    df_out['obv'] = safe_series(obv_ind.on_balance_volume(), df.index)

    # NVI
    nvi_ind = ta.volume.NegativeVolumeIndexIndicator(close, volume)
    df_out['nvi'] = safe_series(nvi_ind.negative_volume_index(), df.index)

    # PVO
    pvo_ind = ta.momentum.PercentageVolumeOscillator(volume)
    df_out['pvo'] = safe_series(pvo_ind.pvo(), df.index)
    df_out['pvo_signal'] = safe_series(pvo_ind.pvo_signal(), df.index)

    # CMF
    cmf_ind = ta.volume.ChaikinMoneyFlowIndicator(high, low, close, volume)
    df_out['cmf'] = safe_series(cmf_ind.chaikin_money_flow(), df.index)

    # SMA200 - Destek (15 dakikalık veri gerektiği için indirip hesaplama yapılacak)
    # Burada 15 dakikalık veri indirip SMA200 hesaplıyoruz
    df_15m = get_data_15m()
    close_15m = df_15m['Close'].squeeze()
    sma200_ind = ta.trend.SMAIndicator(close_15m, window=200)
    sma200_vals = safe_series(sma200_ind.sma_indicator(), df_15m.index)
    # 1 saatlik index ile 15 dakikalık index uyuşmaz, forward fill ile en yakın değeri alıyoruz
    df_out['sma200_15m'] = sma200_vals.reindex(df_out.index, method='ffill')

    return df_out.dropna()

def extract_best_thresholds(df_ind):
    pd.options.display.float_format = '{:,.4f}'.format

    long_zone = df_ind[df_ind['Close'] < df_ind['kc_lower']]
    short_zone = df_ind[df_ind['Close'] > df_ind['kc_upper']]

    # Kullandığınız tüm indikatörleri buraya ekleyebilirsiniz
    indicators = [
        'ema20', 'ema50', 'adx', 'plus_di', 'minus_di', 'stc', 'psar', 'trix',
        'stoch_rsi_k', 'stoch_rsi_d', 'obv', 'nvi', 'pvo', 'pvo_signal', 'cmf', 'sma200_15m'
    ]

    print("== Keltner Channel ALT bandı dışında (LONG sinyali olasılığı) ==")
    print(long_zone[indicators].median())

    print("\n== Keltner Channel ÜST bandı dışında (SHORT sinyali olasılığı) ==")
    print(short_zone[indicators].median())

def main():
    print("Veri indiriliyor...")
    df = get_data()
    print("İndikatörler hesaplanıyor...")
    df_ind = compute_all_indicators(df)
    extract_best_thresholds(df_ind)

if __name__ == "__main__":
    main()
