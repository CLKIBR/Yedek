from core.client import client, handle_client_error

# USDT cinsinden Futures bakiye sorgulama
def get_balance_usdt():
    try:
        response = client.balance(recvWindow=6000)
        for elem in response:
            if elem['asset'] == 'USDT':
                return float(elem['balance'])
    except Exception as error:
        handle_client_error(error)
        return None
