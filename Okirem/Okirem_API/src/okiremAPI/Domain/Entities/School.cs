using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class School : Entity<Guid>
{
    public string Name { get; set; } = default!; // Okul adı
    public SchoolType Type { get; set; } // Okul tipi
    public string? City { get; set; } // Şehir
    public string? District { get; set; } // İlçe
    public string? Country { get; set; } // Ülke
    public string? AddressLine1 { get; set; } // Adres satırı 1
    public string? AddressLine2 { get; set; } // Adres satırı 2
    public string? PostalCode { get; set; } // Posta kodu
    public string? Phone { get; set; } // Telefon
    public string? Email { get; set; } // E-posta
    public string? WebsiteUrl { get; set; } // Web sitesi URL
    public string? Notes { get; set; } // Notlar
    public Guid? TenantId { get; set; } // Kiracı ID

    // Navigation
    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>(); // Sınıflar
    public virtual ICollection<StudentProfile> StudentProfiles { get; set; } = new List<StudentProfile>(); // Öğrenciler
    public virtual ICollection<TeacherProfile> Teachers { get; set; } = new List<TeacherProfile>(); // Öğretmenler
}

