using NArchitecture.Core.Security.Entities;

namespace Domain.Entities;

public enum GenderType
{
    NotSpecified = 0,
    Male = 1,
    Female = 2,
    Other = 3
}

public enum LanguageType
{
    NotSpecified = 0,
    Turkish = 1,
    English = 2,
    German = 3,
    French = 4
}

public enum PositionType
{
    NotSpecified = 0,
    Student = 1,
    Teacher = 2,
    Admin = 3,
    Parent = 4,
    Other = 5
}

public class User : User<Guid>
{
    // Kişisel Bilgiler
    public string FirstName { get; set; } // Ad
    public string LastName { get; set; } // Soyad
    public string PhoneNumber { get; set; } // Telefon
    public DateTime? BirthDate { get; set; } // Doğum tarihi
    public GenderType Gender { get; set; } // Cinsiyet
    public string Address { get; set; } // Adres
    public string NationalId { get; set; } // TC Kimlik No
    public string ProfileImageUrl { get; set; } // Profil fotoğrafı
    public DateTime RegistrationDate { get; set; } // Kayıt tarihi
    public bool IsActive { get; set; } // Aktiflik durumu
    public DateTime? LastLoginDate { get; set; } // Son giriş tarihi
    public string Institution { get; set; } // Kurum/okul/şirket
    public PositionType Position { get; set; } // Pozisyon
    public LanguageType PreferredLanguage { get; set; } // Tercih edilen dil
    public string Bio { get; set; } // Kısa özgeçmiş/açıklama
    public string SocialLinks { get; set; } // Sosyal medya bağlantıları (JSON veya string)
    // Oyunlaştırma Bilgileri
    public int ExperiencePoints { get; set; } // XP
    public int Level { get; set; } // Seviye
    public int BadgeCount { get; set; } // Rozet sayısı
    public int Coin { get; set; } // Sanal para
    public int AchievementCount { get; set; } // Başarımlar
    public int Streak { get; set; } // Günlük giriş serisi
    public int Rank { get; set; } // Sıralama
    public int CompletedQuests { get; set; } // Tamamlanan görev sayısı
    public string CurrentQuest { get; set; } // Aktif görev adı/id
    public double Progress { get; set; } // Genel ilerleme yüzdesi
    public int TotalLoginCount { get; set; } // Toplam giriş sayısı
    public bool IsEmailVerified { get; set; } // E-posta doğrulama durumu
    public bool IsPhoneVerified { get; set; } // Telefon doğrulama durumu

    public User()
    {
        UserOperationClaims = new HashSet<UserOperationClaim>();
        RefreshTokens = new HashSet<RefreshToken>();
        OtpAuthenticators = new HashSet<OtpAuthenticator>();
        EmailAuthenticators = new HashSet<EmailAuthenticator>();
    }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;
}
