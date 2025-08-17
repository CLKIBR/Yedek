using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ParentStudentLinkConfiguration : IEntityTypeConfiguration<ParentStudentLink>
{
    public void Configure(EntityTypeBuilder<ParentStudentLink> builder)
    {
        builder.ToTable("ParentStudentLinks").HasKey(psl => psl.Id);

        builder.Property(psl => psl.Id).HasColumnName("Id").IsRequired();
        builder.Property(psl => psl.ParentProfileId).HasColumnName("ParentProfileId").IsRequired();
        builder.Property(psl => psl.StudentProfileId).HasColumnName("StudentProfileId").IsRequired();
        builder.Property(psl => psl.Relationship).HasColumnName("Relationship").IsRequired().HasConversion<int>();
        builder.Property(psl => psl.IsPrimary).HasColumnName("IsPrimary").IsRequired();
        builder.Property(psl => psl.TenantId).HasColumnName("TenantId");
        builder.Property(psl => psl.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(psl => psl.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(psl => psl.DeletedDate).HasColumnName("DeletedDate");

        // Navigation and foreign key relationships
        builder.HasOne(psl => psl.ParentProfile)
            .WithMany(pp => pp.StudentLinks)
            .HasForeignKey(psl => psl.ParentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(psl => psl.StudentProfile)
            .WithMany(sp => sp.ParentLinks)
            .HasForeignKey(psl => psl.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(psl => !psl.DeletedDate.HasValue);
    }
}