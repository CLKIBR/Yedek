using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TeacherProfileConfiguration : IEntityTypeConfiguration<TeacherProfile>
{
    public void Configure(EntityTypeBuilder<TeacherProfile> builder)
    {
        builder.ToTable("TeacherProfiles").HasKey(tp => tp.Id);

        builder.Property(tp => tp.Id).HasColumnName("Id").IsRequired();
        builder.Property(tp => tp.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(tp => tp.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
        builder.Property(tp => tp.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
        builder.Property(tp => tp.ProfileImageUrl).HasColumnName("ProfileImageUrl");
        builder.Property(tp => tp.Email).HasColumnName("Email").HasMaxLength(256);
        builder.Property(tp => tp.AlternateEmail).HasColumnName("AlternateEmail").HasMaxLength(256);
        builder.Property(tp => tp.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(tp => tp.AlternatePhoneNumber).HasColumnName("AlternatePhoneNumber");
        builder.Property(tp => tp.Locale).HasColumnName("Locale");
        builder.Property(tp => tp.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(tp => tp.Gender).HasColumnName("Gender").IsRequired().HasConversion<int>();
        builder.Property(tp => tp.BirthDate).HasColumnName("BirthDate");
        builder.Property(tp => tp.Country).HasColumnName("Country");
        builder.Property(tp => tp.City).HasColumnName("City");
        builder.Property(tp => tp.District).HasColumnName("District");
        builder.Property(tp => tp.AddressLine1).HasColumnName("AddressLine1");
        builder.Property(tp => tp.AddressLine2).HasColumnName("AddressLine2");
        builder.Property(tp => tp.PostalCode).HasColumnName("PostalCode");
        builder.Property(tp => tp.LinkedInUrl).HasColumnName("LinkedInUrl");
        builder.Property(tp => tp.TwitterUrl).HasColumnName("TwitterUrl");
        builder.Property(tp => tp.Branch).HasColumnName("Branch").HasMaxLength(100);
        builder.Property(tp => tp.SchoolId).HasColumnName("SchoolId");
        builder.Property(tp => tp.IsArchived).HasColumnName("IsArchived").IsRequired();
        builder.Property(tp => tp.Notes).HasColumnName("Notes");
        builder.Property(tp => tp.TagsJson).HasColumnName("TagsJson");
        builder.Property(tp => tp.MetadataJson).HasColumnName("MetadataJson");
        builder.Property(tp => tp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(tp => tp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(tp => tp.DeletedDate).HasColumnName("DeletedDate");

        // Indexes
        builder.HasIndex(tp => tp.Email).IsUnique(false);

        // Navigation and foreign key relationships
        builder.HasOne(tp => tp.School)
            .WithMany(s => s.Teachers)
            .HasForeignKey(tp => tp.SchoolId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(tp => !tp.DeletedDate.HasValue);
    }
}