import os
import subprocess
from config.settings import VIDEO_OUTPUT_DIR, FFMPEG_PATH, DEFAULT_RESOLUTION, DEFAULT_FPS

def render_video_with_images(subtitles, img_paths, audio_path, title, is_shorts=False) -> str:
    """
    Verilen altyazılar, görseller ve ses ile video oluşturur.
    """
    os.makedirs(VIDEO_OUTPUT_DIR, exist_ok=True)
    safe_title = title if title else "untitled"
    output_path = os.path.join(VIDEO_OUTPUT_DIR, f"video_{safe_title}.mp4")
    resolution = "1080x1920" if is_shorts else DEFAULT_RESOLUTION

    if not img_paths or not all(os.path.exists(p) for p in img_paths):
        print("Arka plan görselleri bulunamadı!")
        return None

    duration_per_sub = 10
    total_subs = len(subtitles)
    num_images = len(img_paths)
    if num_images == 0:
        print("Hiç görsel yok!")
        return None
    subs_per_img = max(1, total_subs // num_images)
    segment_paths = []

    # Her resim için video segmenti oluştur
    for idx, img_path in enumerate(img_paths):
        start = idx * subs_per_img
        end = (idx + 1) * subs_per_img if idx < num_images - 1 else total_subs
        segment_duration = max(1, (end - start) * duration_per_sub)
        segment_path = os.path.abspath(os.path.join(VIDEO_OUTPUT_DIR, f"video_{safe_title}_part{idx+1}.mp4"))
        cmd = [
            FFMPEG_PATH,
            "-loop", "1",
            "-i", img_path,
            "-c:v", "libx264",
            "-t", str(segment_duration),
            "-vf", f"scale={resolution}",
            "-pix_fmt", "yuv420p",
            "-y",
            segment_path
        ]
        result = subprocess.run(cmd, capture_output=True, text=True)
        if result.returncode != 0:
            print(f"FFmpeg segment hatası ({img_path}):", result.stderr)
            return None
        segment_paths.append(segment_path)

    # Segmentleri birleştir
    concat_list_path = os.path.abspath(os.path.join(VIDEO_OUTPUT_DIR, f"concat_{safe_title}.txt"))
    with open(concat_list_path, "w") as f:
        for seg in segment_paths:
            f.write(f"file '{seg}'\n")

    concat_path = os.path.abspath(os.path.join(VIDEO_OUTPUT_DIR, f"video_{safe_title}_main.mp4"))
    concat_cmd = [
        FFMPEG_PATH,
        "-f", "concat",
        "-safe", "0",
        "-i", concat_list_path,
        "-c", "copy",
        "-y",
        concat_path
    ]
    result = subprocess.run(concat_cmd, capture_output=True, text=True)
    if result.returncode != 0:
        print("FFmpeg birleştirme hatası:", result.stderr)
        return None

    # Sesi ekle
    final_video_path = output_path
    merge_cmd = [
        FFMPEG_PATH,
        "-i", concat_path,
        "-i", audio_path,
        "-c:v", "copy",
        "-c:a", "aac",
        "-shortest",
        "-y",
        final_video_path
    ]
    result = subprocess.run(merge_cmd, capture_output=True, text=True)
    if result.returncode != 0:
        print("FFmpeg ses ekleme hatası:", result.stderr)
        return None

    print(f"Video başarıyla oluşturuldu: {final_video_path}")
    # Geçici dosyaları sil
    for seg in segment_paths:
        try:
            if os.path.exists(seg):
                os.remove(seg)
        except Exception as e:
            print(f"Geçici segment silinemedi: {seg} ({e})")
    try:
        if os.path.exists(concat_path):
            os.remove(concat_path)
        if os.path.exists(concat_list_path):
            os.remove(concat_list_path)
    except Exception as e:
        print(f"Geçici dosya silinemedi: {e}")
    return final_video_path