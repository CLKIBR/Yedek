using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FeatureFlagConfiguration : IEntityTypeConfiguration<FeatureFlag>
{
    public void Configure(EntityTypeBuilder<FeatureFlag> builder)
    {
        builder.ToTable("FeatureFlags").HasKey(ff => ff.Id);

        builder.Property(ff => ff.Id).HasColumnName("Id").IsRequired();
        builder.Property(ff => ff.Name).HasColumnName("Name").IsRequired();
        builder.Property(ff => ff.Description).HasColumnName("Description");
        builder.Property(ff => ff.Enabled).HasColumnName("Enabled").IsRequired();
        builder.Property(ff => ff.Metadata).HasColumnName("Metadata");
        builder.Property(ff => ff.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ff => ff.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ff => ff.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ff => !ff.DeletedDate.HasValue);
    }
}