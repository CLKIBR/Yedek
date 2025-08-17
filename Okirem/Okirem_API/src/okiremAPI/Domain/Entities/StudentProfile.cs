using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class StudentProfile : Entity<Guid>
{
    public Guid UserId { get; set; } // Kullanıcı ID
    public string FirstName { get; set; } = default!; // Ad
    public string LastName { get; set; } = default!; // Soyad
    public string? ProfileImageUrl { get; set; } // Profil resmi URL
    public string? Email { get; set; } // E-posta
    public string? AlternateEmail { get; set; } // Alternatif e-posta
    public string? PhoneNumber { get; set; } // Telefon numarası
    public string? AlternatePhoneNumber { get; set; } // Alternatif telefon numarası
    public string? Locale { get; set; } // Dil/yerel ayar
    public bool IsActive { get; set; } // Aktiflik durumu
    public GenderType Gender { get; set; } // Cinsiyet
    public DateTime? BirthDate { get; set; } // Doğum tarihi
    public string? Country { get; set; } // Ülke
    public string? City { get; set; } // Şehir
    public string? District { get; set; } // İlçe
    public string? AddressLine1 { get; set; } // Adres satırı 1
    public string? AddressLine2 { get; set; } // Adres satırı 2
    public string? PostalCode { get; set; } // Posta kodu
    public string? LinkedInUrl { get; set; } // LinkedIn URL
    public string? TwitterUrl { get; set; } // Twitter URL
    public Guid? SchoolId { get; set; } // Okul ID
    public Guid? ClassroomId { get; set; } // Sınıf ID
    public string? SchoolNumber { get; set; } // Okul numarası
    public int? GradeLevel { get; set; } // Sınıf seviyesi
    public DateTime? EnrollmentDate { get; set; } // Kayıt tarihi
    public bool IsArchived { get; set; } // Arşiv durumu
    public int Level { get; set; } // Oyun seviyesi
    public int XP { get; set; } // Deneyim puanı
    public int TotalPoints { get; set; } // Toplam puan
    public string? BadgesJson { get; set; } // Rozetler JSON
    public int StreakDays { get; set; } // Seri gün sayısı
    public DateTime? LastActivityAt { get; set; } // Son aktivite tarihi
    public int CompletedTaskCount { get; set; } // Tamamlanan görev sayısı
    public decimal AverageScore { get; set; } // Ortalama puan
    public DateTime? LastCourseAccessAt { get; set; } // Son kurs erişim tarihi
    public string? Notes { get; set; } // Notlar
    public string? TagsJson { get; set; } // Etiketler JSON
    public string? MetadataJson { get; set; } // Ek veriler JSON

    // Navigation
    public virtual User User { get; set; } = default!; // İlgili kullanıcı
    public virtual School? School { get; set; } // İlgili okul
    public virtual Classroom? Classroom { get; set; } // İlgili sınıf
    public virtual ICollection<ParentStudentLink> ParentLinks { get; set; } = new List<ParentStudentLink>(); // Veli bağlantıları
    public virtual ICollection<TeacherStudentLink> TeacherLinks { get; set; } = new List<TeacherStudentLink>(); // Öğretmen bağlantıları
}

