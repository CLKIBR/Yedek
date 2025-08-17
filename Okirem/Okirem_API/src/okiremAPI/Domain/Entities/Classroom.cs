using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Classroom : Entity<Guid>
{
    public string Name { get; set; } = default!; // Sınıf adı
    public Guid SchoolId { get; set; } // Okul ID
    public string? Grade { get; set; } // Sınıf seviyesi
    public string? Notes { get; set; } // Notlar
    public Guid? TenantId { get; set; } // Kiracı ID

    // Navigation
    public virtual School School { get; set; } = default!; // Bağlı olduğu okul
    public virtual ICollection<StudentProfile> StudentProfiles { get; set; } = new List<StudentProfile>(); // Öğrenciler
    public virtual ICollection<TeacherStudentLink> TeacherLinks { get; set; } = new List<TeacherStudentLink>(); // Öğretmen-öğrenci bağlantıları
}
