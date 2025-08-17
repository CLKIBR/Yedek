import os
import re
import math
from config.settings import CAPTION_DIR

def split_sentences(text):
    sentences = re.split(r'(?<=[.!?])\s+', text.strip())
    return [s for s in sentences if s]

def generate_srt(text: str, filename: str = None, title: str = None, ms_per_char: float = 67.81) -> str:
    os.makedirs(CAPTION_DIR, exist_ok=True)
    if filename is None:
        if title:
            filename = f"{title}.srt"
        else:
            filename = "default.srt"
    srt_path = os.path.join(CAPTION_DIR, filename)

    if os.path.exists(srt_path):
        print(f"Altyazı dosyası zaten mevcut: {srt_path}")
        return srt_path

    sentences = split_sentences(text)

    with open(srt_path, "w", encoding="utf-8") as f:
        current_time = 0.0
        for i, sentence in enumerate(sentences, 1):
            duration = len(sentence) * ms_per_char / 1000
            end_time = current_time + duration
            # Yukarı yuvarla
            start_total_sec = math.ceil(current_time)
            end_total_sec = math.ceil(end_time)
            start_min = start_total_sec // 60
            start_sec = start_total_sec % 60
            end_min = end_total_sec // 60
            end_sec = end_total_sec % 60
            f.write(f"{i}\n")
            f.write(f"00:{start_min:02}:{start_sec:02} --> 00:{end_min:02}:{end_sec:02}\n")
            f.write(sentence + "\n\n")
            current_time = end_time
    print(f"Altyazı dosyası oluşturuldu: {srt_path}")
    return srt_path