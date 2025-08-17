from openai import OpenAI
from config.settings import OPENAI_API_KEY
from content.prompts import (
    VIDEO_SCRIPT_SYSTEM_PROMPT,
    SHORTS_SCRIPT_SYSTEM_PROMPT,
    METADATA_SYSTEM_PROMPT,
    TOPIC_SYSTEM_PROMPT,
    SHORTS_METADATA_SYSTEM_PROMPT
)
import os
import json
import re

os.environ["OPENAI_API_KEY"] = OPENAI_API_KEY
client = OpenAI()

def yesgenerate_script(title: str) -> str:
    try:
        response = client.chat.completions.create(
            model="gpt-4.1",
            messages=[
                {"role": "system", "content": VIDEO_SCRIPT_SYSTEM_PROMPT},
                {"role": "user", "content": f"Başlık: {title}"}
            ],
            temperature=0.7
        )
        content = response.choices[0].message.content.strip()
        match = re.search(r"Senaryo:\s*(.*)", content, re.DOTALL)
        if match:
            scenario_text = match.group(1).strip()
        else:
            scenario_text = content
        return scenario_text
    except Exception as e:
        return ""

def generate_shorts_script(title: str) -> str:
    try:
        response = client.chat.completions.create(
            model="gpt-4.1",
            messages=[
                {"role": "system", "content": SHORTS_SCRIPT_SYSTEM_PROMPT},
                {"role": "user", "content": f"Başlık: {title}"}
            ],
            temperature=0.7
        )
        content = response.choices[0].message.content.strip()
        match = re.search(r"Senaryo:\s*(.*)", content, re.DOTALL)
        if match:
            scenario_text = match.group(1).strip()
        else:
            scenario_text = content
        return scenario_text
    except Exception as e:
        return ""

def generate_metadata(topic: str) -> tuple:
    try:
        response = client.chat.completions.create(
            model="gpt-4.1",
            messages=[
                {"role": "system", "content": METADATA_SYSTEM_PROMPT},
                {"role": "user", "content": f"Başlık: {topic}"}
            ],
            temperature=0.7
        )
        content = response.choices[0].message.content.strip()
        return parse_script(content)
    except Exception as e:
        return "", "", []

def generate_shorts_metadata(topic: str) -> tuple:
    try:
        response = client.chat.completions.create(
            model="gpt-4.1",
            messages=[
                {"role": "system", "content": SHORTS_METADATA_SYSTEM_PROMPT},
                {"role": "user", "content": f"Başlık: {topic}"}
            ],
            temperature=0.7
        )
        content = response.choices[0].message.content.strip()
        return parse_script(content)
    except Exception as e:
        return "", "", []

def get_topics(json_path="assets/plans/topics.json"):
    if os.path.exists(json_path):
        try:
            with open(json_path, "r", encoding="utf-8") as f:
                topics = json.load(f)
            return topics
        except Exception as e:
            return []
    try:
        response = client.chat.completions.create(
            model="gpt-4.1",
            messages=[
                {"role": "system", "content": TOPIC_SYSTEM_PROMPT}
            ],
            temperature=0.7
        )
        topics = [t.strip() for t in response.choices[0].message.content.split('\n') if t.strip()]
        os.makedirs(os.path.dirname(json_path), exist_ok=True)
        with open(json_path, "w", encoding="utf-8") as f:
            json.dump(topics[:31], f, ensure_ascii=False, indent=2)
        return topics[:31]
    except Exception as e:
        return []

def parse_script(script: str):
    lines = script.split('\n')
    title = next((l.replace("Başlık:", "").strip() for l in lines if l.lower().startswith("başlık:")), "")
    description = next((l.replace("Açıklama:", "").strip() for l in lines if l.lower().startswith("açıklama:")), "")
    tags_line = next((l.replace("Etiketler:", "").strip() for l in lines if l.lower().startswith("etiketler:")), "")
    tags = [t.strip() for t in tags_line.split(',') if t.strip()]
    if not title or not description or not tags:
        pass
    return title, description, tags

def safe_filename(text):
    # Türkçe ve özel karakterleri temizle
    return (
        text.replace(" ", "_")
        .replace("ı", "i").replace("ş", "s").replace("ç", "c")
        .replace("ğ", "g").replace("ü", "u").replace("ö", "o")
        .replace("İ", "I").replace("!", "").replace("?", "")
        .replace(":", "").replace(".", "").replace(",", "")
    )

def download_images_for_subtitles(title, scenario_text, image_count=4):
    """
    Kullanıcıdan birden fazla arka plan resmi yüklemesi istenir.
    Her resim için dosya adı ve açıklama gösterilir, yükleme tamamlanınca 'yes' ile devam edilir.
    Kullanıcı 'exit' yazarak işlemi iptal edebilir.
    Her resim için senaryo metni parçalara bölünerek açıklama olarak gösterilir.
    """
    out_dir = os.path.join("assets", "images", safe_filename(title))
    os.makedirs(out_dir, exist_ok=True)
    img_paths = []

    # Senaryo metnini cümlelere böl
    scenario_parts = re.split(r'(?<=[.!?])\s+', scenario_text.strip())
    if len(scenario_parts) < image_count:
        # Eksikse boş string ile tamamla
        scenario_parts += [""] * (image_count - len(scenario_parts))

    for i in range(1, image_count + 1):
        img_filename = f"{safe_filename(title)}_{i}.png"
        img_path = os.path.join(out_dir, img_filename)
        scenario_desc = scenario_parts[i-1] if i-1 < len(scenario_parts) else ""
        print(f"\n{i}. Resim için açıklama: {scenario_desc}")
        print(f"{i}. Resim dosya adı: {img_filename}")
        print(f"Lütfen arka plan resmini şuraya yükleyin: {img_path}")
        print("Yükleme işlemi tamamlandığında konsola 'yes' yazın ve Enter'a basın.")
        print("İşlemi iptal etmek için 'exit' yazabilirsiniz.")

        while True:
            user_input = input(f"{i}. Arka plan resmi yüklendi mi? (yes/no/exit): ").strip().lower()
            if user_input == "yes":
                if os.path.exists(img_path):
                    img_paths.append(img_path)
                    print(f"{img_path} başarıyla eklendi.")
                    break
                else:
                    print(f"{img_path} bulunamadı, lütfen dosyayı doğru yere yükleyin.")
            elif user_input == "exit":
                print("İşlem kullanıcı tarafından iptal edildi.")
                return img_paths
            else:
                print("Yükleme bekleniyor...")

    return img_paths