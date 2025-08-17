import requests
import pandas as pd
import json
import os
from settings import OUTPUT_JSON, OUTPUT_CSV

BASE_API = "https://www.meb.gov.tr/baglantilar/okullar/okullar_ajax.php"

HEADERS = {
    "User-Agent": "Mozilla/5.0",
    "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
    "X-Requested-With": "XMLHttpRequest",
    "Referer": "https://www.meb.gov.tr/baglantilar/okullar/"
}

def get_all_schools():
    """Tüm Türkiye okul listesini tek istekte çeker"""
    payload = {
        "draw": 1,
        "columns[0][data]": "OKUL_ADI",
        "columns[0][name]": "",
        "columns[0][searchable]": "true",
        "columns[0][orderable]": "true",
        "columns[0][search][value]": "",
        "columns[0][search][regex]": "false",
        "columns[1][data]": "OKUL_ADI",
        "columns[1][name]": "",
        "columns[1][searchable]": "true",
        "columns[1][orderable]": "true",
        "columns[1][search][value]": "",
        "columns[1][search][regex]": "false",
        "columns[2][data]": "OKUL_ADI",
        "columns[2][name]": "",
        "columns[2][searchable]": "true",
        "columns[2][orderable]": "true",
        "columns[2][search][value]": "",
        "columns[2][search][regex]": "false",
        "order[0][column]": 0,
        "order[0][dir]": "asc",
        "order[0][name]": "",
        "start": 0,
        "length": 60000,  # tüm veriyi çekmek için yüksek değer
        "search[value]": "",
        "search[regex]": "false",
        "il": "",
        "ilce": "",
        "kurumtur": ""
    }
    r = requests.post(BASE_API, headers=HEADERS, data=payload)
    print("HTTP Status:", r.status_code)
    print("First 200 chars of response:", r.text[:200])  # debug amaçlı
    r.raise_for_status()
    data = r.json()
    return data.get("data", [])

def main():
    os.makedirs("output", exist_ok=True)

    print("📥 Tüm Türkiye okul listesi çekiliyor...")
    okullar = get_all_schools()
    print(f"✅ {len(okullar)} okul kaydedildi.")

    # JSON kaydet
    with open(OUTPUT_JSON, "w", encoding="utf-8") as f:
        json.dump(okullar, f, ensure_ascii=False, indent=2)

    # CSV kaydet
    df = pd.DataFrame(okullar)
    df.to_csv(OUTPUT_CSV, index=False, encoding="utf-8-sig")

if __name__ == "__main__":
    main()
