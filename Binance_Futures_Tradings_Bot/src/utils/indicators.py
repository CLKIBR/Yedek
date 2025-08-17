import numpy as np
import pandas as pd # type: ignore

def calculate_rsi(closes, period=14):
    delta = np.diff(closes)
    gain = np.maximum(delta, 0)
    loss = np.abs(np.minimum(delta, 0))
    avg_gain = pd.Series(gain).rolling(window=period).mean()
    avg_loss = pd.Series(loss).rolling(window=period).mean()
    rs = avg_gain / avg_loss
    rsi = 100 - (100 / (1 + rs))
    return rsi.iloc[-1] if not rsi.empty else None

def calculate_macd(closes, fast=12, slow=26, signal=9):
    fast_ema = pd.Series(closes).ewm(span=fast, adjust=False).mean()
    slow_ema = pd.Series(closes).ewm(span=slow, adjust=False).mean()
    macd_line = fast_ema - slow_ema
    signal_line = macd_line.ewm(span=signal, adjust=False).mean()
    return macd_line.iloc[-1], signal_line.iloc[-1]

def calculate_bollinger_bands(closes, period=20, num_std_dev=2):
    series = pd.Series(closes)
    sma = series.rolling(window=period).mean()
    std = series.rolling(window=period).std()
    upper_band = sma + (num_std_dev * std)
    lower_band = sma - (num_std_dev * std)
    return upper_band.iloc[-1], lower_band.iloc[-1], series.iloc[-1]

def calculate_ema(closes, period=50):
    ema = pd.Series(closes).ewm(span=period, adjust=False).mean()
    return ema.iloc[-1]

# ----- SUPER TREND -----

def ATR(df, period=10):
    df = df.copy()
    df['H-L'] = df['High'] - df['Low']
    df['H-PC'] = abs(df['High'] - df['Close'].shift(1))
    df['L-PC'] = abs(df['Low'] - df['Close'].shift(1))
    df['TR'] = df[['H-L', 'H-PC', 'L-PC']].max(axis=1)
    atr = df['TR'].rolling(window=period, min_periods=1).mean()
    return atr

def calculate_supertrend(df, period=10, multiplier=3):
    df = df.copy()
    atr = ATR(df, period)
    hl2 = (df['High'] + df['Low']) / 2
    upperband = hl2 - (multiplier * atr)
    lowerband = hl2 + (multiplier * atr)

    df['final_upperband'] = upperband
    df['final_lowerband'] = lowerband
    df['supertrend'] = True

    for i in range(1, len(df)):
        if df['Close'][i-1] > df['final_upperband'][i-1]:
            df.at[i, 'final_upperband'] = max(upperband[i], df['final_upperband'][i-1])
        else:
            df.at[i, 'final_upperband'] = upperband[i]

        if df['Close'][i-1] < df['final_lowerband'][i-1]:
            df.at[i, 'final_lowerband'] = min(lowerband[i], df['final_lowerband'][i-1])
        else:
            df.at[i, 'final_lowerband'] = lowerband[i]

        if df['supertrend'][i-1]:
            if df['Close'][i] < df['final_upperband'][i]:
                df.at[i, 'supertrend'] = False
            else:
                df.at[i, 'supertrend'] = True
        else:
            if df['Close'][i] > df['final_lowerband'][i]:
                df.at[i, 'supertrend'] = True
            else:
                df.at[i, 'supertrend'] = False

    return df
