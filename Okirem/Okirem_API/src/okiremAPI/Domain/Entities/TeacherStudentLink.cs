using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class TeacherStudentLink : Entity<Guid>
{
    public Guid TeacherProfileId { get; set; } // Öğretmen profil ID
    public Guid StudentProfileId { get; set; } // Öğrenci profil ID
    public Guid? ClassroomId { get; set; } // Sınıf ID
    public string? AcademicYear { get; set; } // Akademik yıl
    public LinkRoleType LinkRole { get; set; } // Bağlantı rolü
    public DateTime? EffectiveFrom { get; set; } // Başlangıç tarihi
    public DateTime? EffectiveTo { get; set; } // Bitiş tarihi
    public bool IsPrimary { get; set; } // Birincil bağlantı mı
    public string? Notes { get; set; } // Notlar
    public Guid? TenantId { get; set; } // Kiracı ID

    // Navigation
    public virtual TeacherProfile TeacherProfile { get; set; } = default!; // Öğretmen profili
    public virtual StudentProfile StudentProfile { get; set; } = default!; // Öğrenci profili
    public virtual Classroom? Classroom { get; set; } // Bağlam sınıfı
}
