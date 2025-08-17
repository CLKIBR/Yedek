using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.ToTable("Schools").HasKey(s => s.Id);

        builder.Property(s => s.Id).HasColumnName("Id").IsRequired();
        builder.Property(s => s.Name).HasColumnName("Name").IsRequired().HasMaxLength(255); // Zorunlu ve max 255
        builder.Property(s => s.Type).HasColumnName("Type").IsRequired();
        builder.Property(s => s.City).HasColumnName("City").HasMaxLength(100); // Nullable, max 100
        builder.Property(s => s.District).HasColumnName("District").HasMaxLength(100); // Nullable, max 100
        builder.Property(s => s.Country).HasColumnName("Country").HasMaxLength(100); // Nullable, max 100
        builder.Property(s => s.AddressLine1).HasColumnName("AddressLine1");
        builder.Property(s => s.AddressLine2).HasColumnName("AddressLine2");
        builder.Property(s => s.PostalCode).HasColumnName("PostalCode");
        builder.Property(s => s.Phone).HasColumnName("Phone");
        builder.Property(s => s.Email).HasColumnName("Email");
        builder.Property(s => s.WebsiteUrl).HasColumnName("WebsiteUrl");
        builder.Property(s => s.Notes).HasColumnName("Notes").HasMaxLength(500); // Nullable, max 500
        builder.Property(s => s.TenantId).HasColumnName("TenantId");
        builder.Property(s => s.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(s => s.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(s => s.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(s => !s.DeletedDate.HasValue);
    }
}