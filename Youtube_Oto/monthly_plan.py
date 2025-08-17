import json
import os
import time
from generator.script_generator import (
    generate_metadata,
    generate_shorts_metadata,
    get_topics
)
from config.settings import OPENAI_API_KEY

os.environ["OPENAI_API_KEY"] = OPENAI_API_KEY

def load_existing_plan(save_path):
    if os.path.exists(save_path):
        with open(save_path, "r", encoding="utf-8") as f:
            try:
                return json.load(f)
            except Exception:
                return []
    return []

def save_plan(plan, save_path):
    with open(save_path, "w", encoding="utf-8") as f:
        json.dump(plan, f, ensure_ascii=False, indent=2)

def safe_generate(func, *args, retries=5, wait=25):
    for attempt in range(retries):
        try:
            return func(*args)
        except Exception as e:
            if "rate_limit" in str(e).lower() or "429" in str(e):
                time.sleep(wait)
            else:
                break
    return None

def generate_monthly_plan():
    save_dir = os.path.join("assets", "plans")
    os.makedirs(save_dir, exist_ok=True)
    save_path = os.path.join(save_dir, "monthly_plan.json")

    # Başlangıç tarihi: 2025-07-21
    start_year = 2025
    start_month = 7
    start_day = 21

    topics = get_topics()

    existing_plan = load_existing_plan(save_path)
    existing_dates = {item["date"] for item in existing_plan}
    plan = existing_plan.copy()

    for i, topic in enumerate(topics):
        # Tarih hesaplama
        day = start_day + i
        month = start_month
        year = start_year
        # Ay sonunu geçerse ayı ve yılı artır
        while day > 31:
            day -= 31
            month += 1
            if month > 12:
                month = 1
                year += 1
        date = f"{year}-{str(month).zfill(2)}-{str(day).zfill(2)}"
        if date in existing_dates:
            continue
        try:
            main_title, main_desc, main_tags = safe_generate(generate_metadata, topic)
            if not main_title:
                continue
            main = {
                "title": main_title,
                "description": main_desc,
                "tags": main_tags,
                "thumbnail_file": f"{topic.replace(' ', '_').lower()}_main.png",
                "categoryId": "24",
                "defaultLanguage": "tr",
                "privacyStatus": "public",
                "madeForKids": True,
                "selfDeclaredMadeForKids": True,
                "type": "main",
                "publishTime": f"16:00:00"
            }
            shorts = []
            for j, hour in enumerate([9, 12, 18]):
                shorts_response = safe_generate(generate_shorts_metadata, topic)
                if not shorts_response or not isinstance(shorts_response, tuple):
                    continue
                shorts_title, shorts_desc, shorts_tags = shorts_response
                shorts.append({
                    "title": shorts_title,
                    "description": shorts_desc,
                    "tags": shorts_tags,
                    "thumbnail_file": f"{topic.replace(' ', '_').lower()}_short{j+1}.png",
                    "categoryId": "24",
                    "defaultLanguage": "tr",
                    "privacyStatus": "public",
                    "madeForKids": True,
                    "selfDeclaredMadeForKids": True,
                    "type": "short",
                    "publishTime": f"{str(hour).zfill(2)}:00:00"
                })
            plan.append({
                "date": date,
                "main": main,
                "shorts": shorts
            })
            save_plan(plan, save_path)
        except Exception:
            pass

if __name__ == "__main__":
    generate_monthly_plan()