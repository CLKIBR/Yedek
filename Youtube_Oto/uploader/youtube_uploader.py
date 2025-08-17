import os
import pickle
from google_auth_oauthlib.flow import InstalledAppFlow
from googleapiclient.discovery import build
from googleapiclient.http import MediaFileUpload
from config.settings import (
    YOUTUBE_CLIENT_SECRET_FILE,
    YOUTUBE_CREDENTIALS_FILE,
    YOUTUBE_CATEGORY_ID,
    YOUTUBE_PRIVACY
)

SCOPES = ["https://www.googleapis.com/auth/youtube.upload"]

def get_authenticated_service():
    creds = None
    if os.path.exists(YOUTUBE_CREDENTIALS_FILE):
        with open(YOUTUBE_CREDENTIALS_FILE, "rb") as token:
            creds = pickle.load(token)
    if not creds or not creds.valid:
        flow = InstalledAppFlow.from_client_secrets_file(YOUTUBE_CLIENT_SECRET_FILE, SCOPES)
        creds = flow.run_local_server(port=0)
        with open(YOUTUBE_CREDENTIALS_FILE, "wb") as token:
            pickle.dump(creds, token)
    return build("youtube", "v3", credentials=creds)

def upload_video(video_file: str, title: str, description: str, tags: list, is_short=False, thumbnail_path=None):
    service = get_authenticated_service()
    body = {
        "snippet": {
            "title": title + (" #Shorts" if is_short else ""),
            "description": description,
            "tags": tags,
            "categoryId": YOUTUBE_CATEGORY_ID,
        },
        "status": {
            "privacyStatus": YOUTUBE_PRIVACY,
            "selfDeclaredMadeForKids": False,
        }
    }

    media = MediaFileUpload(video_file, chunksize=-1, resumable=True)

    try:
        request = service.videos().insert(
            part="snippet,status",
            body=body,
            media_body=media
        )

        response = None
        while response is None:
            status, response = request.next_chunk()
            if status:
                print(f"Yükleniyor: %{int(status.progress() * 100)}")

        print(f"✅ Video yüklendi: {response.get('id')}")

        # Thumbnail yükle
        if thumbnail_path and os.path.exists(thumbnail_path):
            try:
                service.thumbnails().set(
                    videoId=response.get('id'),
                    media_body=MediaFileUpload(thumbnail_path)
                ).execute()
                print("✅ Thumbnail başarıyla yüklendi.")
            except Exception as e:
                print(f"Thumbnail yüklenirken hata oluştu: {e}")

        return response.get("id")
    except Exception as e:
        print(f"Video yüklenirken hata oluştu: {e}")
        return None