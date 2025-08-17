import requests
import os
from config.settings import ELEVENLABS_API_KEY, ELEVENLABS_VOICE_ID, VOICE_DIR

def text_to_speech(text: str, voice_id=ELEVENLABS_VOICE_ID, title: str = None) -> str:
    os.makedirs(VOICE_DIR, exist_ok=True)
    if not title:
        title = "audio_" + str(hash(text))
    audio_path = os.path.join(VOICE_DIR, f"{title}.mp3")

    # Eğer dosya zaten varsa tekrar oluşturma, doğrudan yolu döndür
    if os.path.exists(audio_path):
        print(f"Ses dosyası zaten mevcut: {audio_path}")
        return audio_path

    url = f"https://api.elevenlabs.io/v1/text-to-speech/{voice_id}"
    headers = {
        "xi-api-key": ELEVENLABS_API_KEY,
        "Content-Type": "application/json"
    }
    payload = {
        "text": text,
        "voice_settings": {
            "stability": 0.5,
            "similarity_boost": 0.8
        }
    }

    try:
        response = requests.post(url, headers=headers, json=payload)
        response.raise_for_status()
        with open(audio_path, "wb") as f:
            f.write(response.content)
        print(f"Ses dosyası oluşturuldu: {audio_path}")
        return audio_path
    except Exception as e:
        print(f"Ses dosyası oluşturulamadı! Hata: {e}")
        return None