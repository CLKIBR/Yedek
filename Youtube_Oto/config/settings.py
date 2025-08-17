import os
from dotenv import load_dotenv

load_dotenv()

OPENAI_API_KEY = os.getenv("OPENAI_API_KEY")
ELEVENLABS_API_KEY = os.getenv("ELEVENLABS_API_KEY")
#ELEVENLABS_VOICE_ID = "GEN2E12527w9Oljwn8V6"
ELEVENLABS_VOICE_ID = "xsGHrtxT5AdDzYXTQT0d"

YOUTUBE_CLIENT_SECRET_FILE = "client_secret_373287744329-8h60bpr7sranjv74akflr2i8vmfhpt1l.apps.googleusercontent.com (1).json"
YOUTUBE_CREDENTIALS_FILE = "youtube_token.json"

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
VOICE_DIR = os.path.join(BASE_DIR, "..", "data", "voice_cache")
VIDEO_OUTPUT_DIR = os.path.join(BASE_DIR, "..", "data", "rendered_videos")
THUMBNAIL_DIR = os.path.join(BASE_DIR, "..", "data", "thumbnails")
CAPTION_DIR = os.path.join(BASE_DIR, "..", "data", "captions")
ASSETS_DIR = os.path.join(BASE_DIR, "..", "assets")
FONTS_DIR = os.path.join(ASSETS_DIR, "fonts")

FFMPEG_PATH = "C:/ffmpeg/bin/ffmpeg.exe"
DEFAULT_RESOLUTION = "1920x1080"
DEFAULT_FPS = 30

YOUTUBE_CATEGORY_ID = "22"
YOUTUBE_PRIVACY = "public"
SHORTS_MAX_DURATION = 60

LOG_FILE = os.path.join(BASE_DIR, "..", "log.txt")

