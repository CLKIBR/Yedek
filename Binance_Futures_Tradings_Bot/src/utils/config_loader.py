import yaml
import os

class ConfigLoader:
    """
    YAML formatındaki yapılandırma dosyasını yüklemek için kullanılan yardımcı sınıf.
    Varsayılan olarak 'config/config.yaml' yolunu okur.
    """

    @staticmethod
    def load_config(path: str = "config/config.yaml") -> dict:
        """
        Belirtilen YAML dosyasını yükler ve bir sözlük (dict) olarak döner.

        Args:
            path (str): YAML dosyasının yolu.

        Returns:
            dict: Yapılandırma verileri.

        Raises:
            RuntimeError: Dosya okunamazsa veya biçimi hatalıysa hata fırlatır.
        """
        if not os.path.exists(path):
            raise RuntimeError(f"Yapılandırma dosyası bulunamadı: {path}")

        try:
            with open(path, "r") as file:
                return yaml.safe_load(file)
        except yaml.YAMLError as e:
            raise RuntimeError(f"YAML ayrıştırma hatası: {e}")
        except Exception as e:
            raise RuntimeError(f"Yapılandırma dosyası yüklenemedi: {e}")
