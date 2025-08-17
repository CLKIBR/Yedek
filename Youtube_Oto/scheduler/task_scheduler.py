import schedule
import time
import json
from datetime import datetime
from scheduler.daily_scheduler import run_daily_production

def job():
    with open("assets/plans/monthly_plan.json", "r", encoding="utf-8") as f:
        plan = json.load(f)

    now = datetime.now()
    today_str = now.strftime("%Y-%m-%d")
    current_hour_minute = now.strftime("%H:%M")

    for item in plan:
        if item["date"] == today_str:
            # Ana video zamanı kontrolü
            main_publish_time = item["main"]["publishTime"][:5]  # "HH:MM"
            if current_hour_minute == main_publish_time:
                print("Ana video zamanı eşleşti, run_daily_production çağrılıyor.")
                run_daily_production(item["main"]) # Tüm main bilgilerini gönde

            # Shorts zamanı kontrolü
            for short in item.get("shorts", []):
                shorts_publish_time = short["publishTime"][:5]  # "HH:MM"
                if current_hour_minute == shorts_publish_time:
                    print(f"Shorts zamanı eşleşti ({short['title']}), run_daily_production çağrılıyor.")
                    run_daily_production(short)  # Tüm short bilgilerini gönder
            break
    else:
        print("Bugüne ait plan bulunamadı.")

def run_scheduler():
    schedule.every(30).seconds.do(job)  # Her 30 saniyede bir kontrol et
    print("Zamanlayıcı başlatıldı.")
    while True:
        schedule.run_pending()
        time.sleep(30)