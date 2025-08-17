import os
import requests
from PIL import Image, ImageFilter
from config.settings import ASSETS_DIR, FONTS_DIR

def download_font(url: str, save_path: str):
    os.makedirs(os.path.dirname(save_path), exist_ok=True)
    if not os.path.exists(save_path):
        print(f"Font indiriliyor: {save_path}")
        r = requests.get(url)
        r.raise_for_status()
        with open(save_path, "wb") as f:
            f.write(r.content)
    else:
        print("Font zaten mevcut.")

def generate_gradient_background(save_path: str, width=1920, height=1080):
    os.makedirs(os.path.dirname(save_path), exist_ok=True)
    if os.path.exists(save_path):
        print("Arka plan görseli zaten mevcut.")
        return

    print("Arka plan görseli oluşturuluyor...")
    base = Image.new("RGB", (width, height))
    top_color = (25, 25, 112)  # Midnight Blue
    bottom_color = (72, 61, 139)  # Dark Slate Blue

    for y in range(height):
        ratio = y / height
        r = int(top_color[0] * (1 - ratio) + bottom_color[0] * ratio)
        g = int(top_color[1] * (1 - ratio) + bottom_color[1] * ratio)
        b = int(top_color[2] * (1 - ratio) + bottom_color[2] * ratio)
        for x in range(width):
            base.putpixel((x, y), (r, g, b))

    base = base.filter(ImageFilter.GaussianBlur(radius=15))
    base.save(save_path)
    print(f"Arka plan görseli kaydedildi: {save_path}")

def setup_assets():
    font_url = "https://raw.githubusercontent.com/google/fonts/main/apache/robotoslab/fonts/ttf/RobotoSlab-Regular.ttf"
    font_path = os.path.join(FONTS_DIR, "RobotoSlab-VariableFont_wght.ttf")
    download_font(font_url, font_path)

    bg_path = os.path.join(ASSETS_DIR, "background.jpg")
    generate_gradient_background(bg_path)
