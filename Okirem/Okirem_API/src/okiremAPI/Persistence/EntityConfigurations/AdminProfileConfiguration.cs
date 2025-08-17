using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AdminProfileConfiguration : IEntityTypeConfiguration<AdminProfile>
{
    public void Configure(EntityTypeBuilder<AdminProfile> builder)
    {
        builder.ToTable("AdminProfiles").HasKey(ap => ap.Id);

        builder.Property(ap => ap.Id).HasColumnName("Id").IsRequired();
        builder.Property(ap => ap.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(ap => ap.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
        builder.Property(ap => ap.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
        builder.Property(ap => ap.ProfileImageUrl).HasColumnName("ProfileImageUrl");
        builder.Property(ap => ap.Email).HasColumnName("Email").HasMaxLength(256);
        builder.Property(ap => ap.AlternateEmail).HasColumnName("AlternateEmail").HasMaxLength(256);
        builder.Property(ap => ap.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(ap => ap.AlternatePhoneNumber).HasColumnName("AlternatePhoneNumber");
        builder.Property(ap => ap.Locale).HasColumnName("Locale");
        builder.Property(ap => ap.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(ap => ap.Gender).HasColumnName("Gender").IsRequired().HasConversion<int>();
        builder.Property(ap => ap.BirthDate).HasColumnName("BirthDate");
        builder.Property(ap => ap.Country).HasColumnName("Country");
        builder.Property(ap => ap.City).HasColumnName("City");
        builder.Property(ap => ap.District).HasColumnName("District");
        builder.Property(ap => ap.AddressLine1).HasColumnName("AddressLine1");
        builder.Property(ap => ap.AddressLine2).HasColumnName("AddressLine2");
        builder.Property(ap => ap.PostalCode).HasColumnName("PostalCode");
        builder.Property(ap => ap.LinkedInUrl).HasColumnName("LinkedInUrl");
        builder.Property(ap => ap.TwitterUrl).HasColumnName("TwitterUrl");
        builder.Property(ap => ap.IsArchived).HasColumnName("IsArchived").IsRequired();
        builder.Property(ap => ap.Notes).HasColumnName("Notes");
        builder.Property(ap => ap.TagsJson).HasColumnName("TagsJson");
        builder.Property(ap => ap.MetadataJson).HasColumnName("MetadataJson");
        builder.Property(ap => ap.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ap => ap.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ap => ap.DeletedDate).HasColumnName("DeletedDate");

        // Indexes
        builder.HasIndex(ap => ap.Email).IsUnique(false);

        // Navigation and foreign key relationships
        builder.HasOne(ap => ap.User)
            .WithMany()
            .HasForeignKey(ap => ap.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(ap => !ap.DeletedDate.HasValue);
    }
}