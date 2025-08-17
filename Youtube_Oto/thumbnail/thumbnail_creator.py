import os
from PIL import Image, ImageDraw, ImageFont
import uuid
import random
import colorsys

THUMBNAIL_DIR = "data/thumbnails"
FONTS_DIR = "assets/fonts"

def random_vivid_color(alpha=120):
    h = random.random()
    s = random.uniform(0.8, 1.0)
    v = random.uniform(0.8, 1.0)
    r, g, b = colorsys.hsv_to_rgb(h, s, v)
    return (int(r*255), int(g*255), int(b*255), alpha)

def random_vivid_text_color():
    h = random.random()
    s = random.uniform(0.7, 1.0)
    v = random.uniform(0.7, 1.0)
    r, g, b = colorsys.hsv_to_rgb(h, s, v)
    return (int(r*255), int(g*255), int(b*255))

def create_thumbnail(title: str, filename: str = None, bg_image_path: str = None, type: str = "main") -> str:
    os.makedirs(THUMBNAIL_DIR, exist_ok=True)
    if type == "short":
        width, height = 720, 1280
    else:
        width, height = 1280, 720
    font_path = os.path.join(FONTS_DIR, "RobotoSlab-VariableFont_wght.ttf")

    # Arka plan olarak ana görseli kullan
    if bg_image_path and os.path.exists(bg_image_path):
        img = Image.open(bg_image_path).convert("RGBA")
        img = img.resize((width, height), Image.LANCZOS)
    else:
        img = Image.new("RGBA", (width, height), color=(20, 20, 20, 255))

    draw = ImageDraw.Draw(img)

    # Başlığı kelimelere ayır
    words = title.split()
    num_words = len(words)

    # Kutuların toplam yüksekliği resmin %80'i olacak şekilde ayarla
    total_box_height = int(height * 0.8)
    box_height = total_box_height // num_words
    box_padding_x = 40
    box_padding_y = 10

    # Kutular arası boşluk (örneğin 10px)
    gap = 10
    total_gaps = gap * (num_words - 1)
    actual_boxes_height = box_height * num_words + total_gaps

    # Her kelime için kutu ve font boyutunu ayarla
    max_box_width = width * 0.9
    font_sizes = []
    word_sizes = []

    # Her kelime için uygun font boyutunu bul
    for word in words:
        font_size = box_height - 2 * box_padding_y
        while font_size > 10:
            try:
                font = ImageFont.truetype(font_path, font_size)
            except IOError:
                font = ImageFont.load_default()
            bbox = draw.textbbox((0, 0), word, font=font)
            text_w = bbox[2] - bbox[0]
            text_h = bbox[3] - bbox[1]
            if text_w <= max_box_width - 2 * box_padding_x and text_h <= box_height - 2 * box_padding_y:
                break
            font_size -= 2
        font_sizes.append(font_size)
        word_sizes.append((text_w, text_h))

    # Kutuları alt alta ortala ve aralarına boşluk ekle
    left_margin = int(width * 0.1)  # %10 sol boşluk
    start_y = (height - actual_boxes_height) // 2
    for i, word in enumerate(words):
        font = ImageFont.truetype(font_path, font_sizes[i])
        text_w, text_h = word_sizes[i]
        box_w = text_w + 2 * box_padding_x
        box_h = box_height
        box_x = left_margin
        box_y = start_y + i * (box_height + gap)

        # Kutunun rengini canlı random seç
        box_color = random_vivid_color(alpha=120)

        # Kutuyu overlay ile çiz
        overlay = Image.new("RGBA", img.size, (0,0,0,0))
        overlay_draw = ImageDraw.Draw(overlay)
        overlay_draw.rectangle([box_x, box_y, box_x + box_w, box_y + box_h], fill=box_color)
        img = Image.alpha_composite(img, overlay)

        draw = ImageDraw.Draw(img)
        # Metni kutu içinde tam ortala (daha hassas)
        bbox = draw.textbbox((0, 0), word, font=font)
        text_w = bbox[2] - bbox[0]
        text_h = bbox[3] - bbox[1]
        text_x = box_x + (box_w - text_w) // 2 - bbox[0]
        text_y = box_y + (box_h - text_h) // 2 - bbox[1]

        # Gölge efekti
        shadow_offset = 4
        draw.text((text_x + shadow_offset, text_y + shadow_offset),
                  word, font=font, fill=(0,0,0,160))
        # Metin rengi canlı random
        text_color = random_vivid_text_color()
        draw.text((text_x, text_y), word, font=font, fill=text_color)

    if filename:
        thumb_path = os.path.join(THUMBNAIL_DIR, filename)
    else:
        thumb_path = os.path.join(THUMBNAIL_DIR, f"thumb_{uuid.uuid4()}.png")
    img.convert("RGB").save(thumb_path)
    return thumb_path