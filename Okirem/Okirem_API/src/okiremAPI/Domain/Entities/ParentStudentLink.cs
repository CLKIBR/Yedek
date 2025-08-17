using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ParentStudentLink : Entity<Guid>
{
    public Guid ParentProfileId { get; set; } // Veli profil ID
    public Guid StudentProfileId { get; set; } // Öğrenci profil ID
    public RelationshipType Relationship { get; set; } // Yakınlık ilişkisi
    public bool IsPrimary { get; set; } // Birincil veli mi
    public Guid? TenantId { get; set; } // Kiracı ID

    // Navigation
    public virtual ParentProfile ParentProfile { get; set; } = default!; // Veli profili
    public virtual StudentProfile StudentProfile { get; set; } = default!; // Öğrenci profili
}
