from generator.script_generator import generate_script, generate_shorts_script, download_images_for_subtitles
from generator.tts_generator import text_to_speech
from generator.video_renderer import render_video_with_images
from uploader.youtube_uploader import upload_video
from generator.caption_generator import generate_srt
from thumbnail.thumbnail_creator import create_thumbnail
import os
import re

def parse_srt_subtitles(srt_path):
    with open(srt_path, "r", encoding="utf-8") as f:
        content = f.read()
    blocks = re.split(r"\n\s*\n", content)
    subtitles = []
    for block in blocks:
        lines = block.strip().split("\n")
        if len(lines) >= 3:
            text = " ".join(lines[2:])
            subtitles.append(text)
    return subtitles

def run_daily_production(content):
    print("🔁 Günlük içerik üretimi başlatılıyor...")

    title = content.get("title", "")
    description = content.get("description", "")
    tags = content.get("tags", [])
    thumbnail_file = content.get("thumbnail_file", "")
    video_type = content.get("type", "")  # "main" veya "short"
    print(f"type: {video_type}")

    image_count = 4  # Her zaman 4 resim yüklensin

    if video_type == "main":
        scenario_text = generate_script(title)
        print(f"Senaryo metni: {scenario_text} + title: {title}")
        srt_path = generate_srt(scenario_text, filename=f"{title}.srt", title=title)
        subtitles = parse_srt_subtitles(srt_path)
        image_paths = download_images_for_subtitles(title, scenario_text, image_count=image_count)
        if not image_paths or len(image_paths) < image_count:
            print("Yeterli arka plan görseli yüklenmedi, video üretimi durduruldu.")
            return False
        thumbnail_path = create_thumbnail(title, thumbnail_file, bg_image_path=image_paths[0], type=video_type)
        audio_path = text_to_speech(scenario_text, title=title)
        if not audio_path or not os.path.exists(audio_path):
            print("Ses dosyası oluşturulamadı, video üretimi durduruldu.")
            return False
        video_path = render_video_with_images(subtitles, image_paths, audio_path, title, is_shorts=False)
        if not video_path or not os.path.exists(video_path):
            print("Video dosyası oluşturulamadı, yükleme işlemi durduruldu.")
            return False
        upload_video(
            video_path,
            title,
            description,
            tags,
            is_short=False,
            thumbnail_path=thumbnail_path
        )
    else:  # Shorts
        scenario_text = generate_shorts_script(title)
        srt_path = generate_srt(scenario_text, filename=f"{title}_shorts.srt", title=title)
        subtitles = parse_srt_subtitles(srt_path)
        image_paths = download_images_for_subtitles(title, scenario_text, image_count=image_count)
        if not image_paths or len(image_paths) < image_count:
            print("Yeterli arka plan görseli yüklenmedi, video üretimi durduruldu.")
            return False
        thumbnail_path = create_thumbnail(title, thumbnail_file, bg_image_path=image_paths[0], type=video_type)
        shorts_audio_path = text_to_speech(scenario_text, title=title)
        if not shorts_audio_path or not os.path.exists(shorts_audio_path):
            print("Shorts ses dosyası oluşturulamadı, video üretimi durduruldu.")
            return False
        shorts_path = render_video_with_images(subtitles, image_paths, shorts_audio_path, title, is_shorts=True)
        if not shorts_path or not os.path.exists(shorts_path):
            print("Shorts video dosyası oluşturulamadı, yükleme işlemi durduruldu.")
            return False
        upload_video(
            shorts_path,
            title + " #Shorts",
            description,
            tags,
            is_short=True,
            thumbnail_path=thumbnail_path
        )

    print("✅ Günlük içerik üretimi tamamlandı.")
    return True