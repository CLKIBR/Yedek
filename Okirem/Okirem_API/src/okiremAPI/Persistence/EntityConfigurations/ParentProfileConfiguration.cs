using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ParentProfileConfiguration : IEntityTypeConfiguration<ParentProfile>
{
    public void Configure(EntityTypeBuilder<ParentProfile> builder)
    {
        builder.ToTable("ParentProfiles").HasKey(pp => pp.Id);

        builder.Property(pp => pp.Id).HasColumnName("Id").IsRequired();
        builder.Property(pp => pp.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(pp => pp.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
        builder.Property(pp => pp.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
        builder.Property(pp => pp.ProfileImageUrl).HasColumnName("ProfileImageUrl");
        builder.Property(pp => pp.Email).HasColumnName("Email").HasMaxLength(256);
        builder.Property(pp => pp.AlternateEmail).HasColumnName("AlternateEmail").HasMaxLength(256);
        builder.Property(pp => pp.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(pp => pp.AlternatePhoneNumber).HasColumnName("AlternatePhoneNumber");
        builder.Property(pp => pp.Locale).HasColumnName("Locale");
        builder.Property(pp => pp.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(pp => pp.Gender).HasColumnName("Gender").IsRequired().HasConversion<int>();
        builder.Property(pp => pp.BirthDate).HasColumnName("BirthDate");
        builder.Property(pp => pp.Country).HasColumnName("Country");
        builder.Property(pp => pp.City).HasColumnName("City");
        builder.Property(pp => pp.District).HasColumnName("District");
        builder.Property(pp => pp.AddressLine1).HasColumnName("AddressLine1");
        builder.Property(pp => pp.AddressLine2).HasColumnName("AddressLine2");
        builder.Property(pp => pp.PostalCode).HasColumnName("PostalCode");
        builder.Property(pp => pp.LinkedInUrl).HasColumnName("LinkedInUrl");
        builder.Property(pp => pp.TwitterUrl).HasColumnName("TwitterUrl");
        builder.Property(pp => pp.IsArchived).HasColumnName("IsArchived").IsRequired();
        builder.Property(pp => pp.Notes).HasColumnName("Notes");
        builder.Property(pp => pp.TagsJson).HasColumnName("TagsJson");
        builder.Property(pp => pp.MetadataJson).HasColumnName("MetadataJson");
        builder.Property(pp => pp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pp => pp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pp => pp.DeletedDate).HasColumnName("DeletedDate");

        // Indexes
        builder.HasIndex(pp => pp.Email).IsUnique(false);

        // Navigation and foreign key relationships
        builder.HasOne(pp => pp.User)
            .WithMany()
            .HasForeignKey(pp => pp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(pp => !pp.DeletedDate.HasValue);
    }
}