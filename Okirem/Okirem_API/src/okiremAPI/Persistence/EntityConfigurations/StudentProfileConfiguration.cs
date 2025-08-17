using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> builder)
    {
        builder.ToTable("StudentProfiles").HasKey(sp => sp.Id);

        builder.Property(sp => sp.Id).HasColumnName("Id").IsRequired();
        builder.Property(sp => sp.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(sp => sp.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
        builder.Property(sp => sp.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
        builder.Property(sp => sp.ProfileImageUrl).HasColumnName("ProfileImageUrl");
        builder.Property(sp => sp.Email).HasColumnName("Email").HasMaxLength(256);
        builder.Property(sp => sp.AlternateEmail).HasColumnName("AlternateEmail").HasMaxLength(256);
        builder.Property(sp => sp.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(sp => sp.AlternatePhoneNumber).HasColumnName("AlternatePhoneNumber");
        builder.Property(sp => sp.Locale).HasColumnName("Locale");
        builder.Property(sp => sp.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(sp => sp.Gender).HasColumnName("Gender").IsRequired().HasConversion<int>();
        builder.Property(sp => sp.BirthDate).HasColumnName("BirthDate");
        builder.Property(sp => sp.Country).HasColumnName("Country");
        builder.Property(sp => sp.City).HasColumnName("City");
        builder.Property(sp => sp.District).HasColumnName("District");
        builder.Property(sp => sp.AddressLine1).HasColumnName("AddressLine1");
        builder.Property(sp => sp.AddressLine2).HasColumnName("AddressLine2");
        builder.Property(sp => sp.PostalCode).HasColumnName("PostalCode");
        builder.Property(sp => sp.LinkedInUrl).HasColumnName("LinkedInUrl");
        builder.Property(sp => sp.TwitterUrl).HasColumnName("TwitterUrl");
        builder.Property(sp => sp.SchoolId).HasColumnName("SchoolId");
        builder.Property(sp => sp.ClassroomId).HasColumnName("ClassroomId");
        builder.Property(sp => sp.SchoolNumber).HasColumnName("SchoolNumber").HasMaxLength(30);
        builder.Property(sp => sp.GradeLevel).HasColumnName("GradeLevel");
        builder.Property(sp => sp.EnrollmentDate).HasColumnName("EnrollmentDate");
        builder.Property(sp => sp.IsArchived).HasColumnName("IsArchived").IsRequired();
        builder.Property(sp => sp.Level).HasColumnName("Level").IsRequired();
        builder.Property(sp => sp.XP).HasColumnName("XP").IsRequired();
        builder.Property(sp => sp.TotalPoints).HasColumnName("TotalPoints").IsRequired();
        builder.Property(sp => sp.BadgesJson).HasColumnName("BadgesJson");
        builder.Property(sp => sp.StreakDays).HasColumnName("StreakDays").IsRequired();
        builder.Property(sp => sp.LastActivityAt).HasColumnName("LastActivityAt");
        builder.Property(sp => sp.CompletedTaskCount).HasColumnName("CompletedTaskCount").IsRequired();
        builder.Property(sp => sp.AverageScore)
            .HasColumnName("AverageScore")
            .IsRequired()
            .HasPrecision(18, 4); // Decimal hassasiyet ve ölçek belirtildi
        builder.Property(sp => sp.LastCourseAccessAt).HasColumnName("LastCourseAccessAt");
        builder.Property(sp => sp.Notes).HasColumnName("Notes");
        builder.Property(sp => sp.TagsJson).HasColumnName("TagsJson");
        builder.Property(sp => sp.MetadataJson).HasColumnName("MetadataJson");
        builder.Property(sp => sp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(sp => sp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(sp => sp.DeletedDate).HasColumnName("DeletedDate");

        // Indexes
        builder.HasIndex(sp => sp.Email).IsUnique(false);
        builder.HasIndex(sp => sp.SchoolNumber).IsUnique(false);

        // Navigation and foreign key relationships
        builder.HasOne(sp => sp.School)
            .WithMany(s => s.StudentProfiles)
            .HasForeignKey(sp => sp.SchoolId)
            .OnDelete(DeleteBehavior.NoAction); 

        builder.HasOne(sp => sp.Classroom)
            .WithMany(s => s.StudentProfiles)
            .HasForeignKey(sp => sp.ClassroomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(sp => !sp.DeletedDate.HasValue);
    }
}