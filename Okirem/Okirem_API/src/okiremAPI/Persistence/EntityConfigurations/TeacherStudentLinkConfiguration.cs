using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TeacherStudentLinkConfiguration : IEntityTypeConfiguration<TeacherStudentLink>
{
    public void Configure(EntityTypeBuilder<TeacherStudentLink> builder)
    {
        builder.ToTable("TeacherStudentLinks").HasKey(tsl => tsl.Id);

        builder.Property(tsl => tsl.Id).HasColumnName("Id").IsRequired();
        builder.Property(tsl => tsl.TeacherProfileId).HasColumnName("TeacherProfileId").IsRequired();
        builder.Property(tsl => tsl.StudentProfileId).HasColumnName("StudentProfileId").IsRequired();
        builder.Property(tsl => tsl.ClassroomId).HasColumnName("ClassroomId");
        builder.Property(tsl => tsl.AcademicYear).HasColumnName("AcademicYear").HasMaxLength(20);
        builder.Property(tsl => tsl.LinkRole).HasColumnName("LinkRole").IsRequired().HasConversion<int>();
        builder.Property(tsl => tsl.EffectiveFrom).HasColumnName("EffectiveFrom");
        builder.Property(tsl => tsl.EffectiveTo).HasColumnName("EffectiveTo");
        builder.Property(tsl => tsl.IsPrimary).HasColumnName("IsPrimary").IsRequired();
        builder.Property(tsl => tsl.Notes).HasColumnName("Notes").HasMaxLength(500);
        builder.Property(tsl => tsl.TenantId).HasColumnName("TenantId");
        builder.Property(tsl => tsl.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(tsl => tsl.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(tsl => tsl.DeletedDate).HasColumnName("DeletedDate");

        // Unique index for TeacherProfileId, StudentProfileId, AcademicYear
        builder.HasIndex(tsl => new { tsl.TeacherProfileId, tsl.StudentProfileId, tsl.AcademicYear }).IsUnique();

        // Navigation and foreign key relationships
        builder.HasOne(tsl => tsl.TeacherProfile)
            .WithMany(tp => tp.StudentLinks)
            .HasForeignKey(tsl => tsl.TeacherProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tsl => tsl.StudentProfile)
            .WithMany(sp => sp.TeacherLinks)
            .HasForeignKey(tsl => tsl.StudentProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tsl => tsl.Classroom)
            .WithMany(c => c.TeacherLinks)
            .HasForeignKey(tsl => tsl.ClassroomId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(tsl => !tsl.DeletedDate.HasValue);
    }
}