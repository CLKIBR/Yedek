using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TeacherParentLinkConfiguration : IEntityTypeConfiguration<TeacherParentLink>
{
    public void Configure(EntityTypeBuilder<TeacherParentLink> builder)
    {
        builder.ToTable("TeacherParentLinks").HasKey(tpl => tpl.Id);

        builder.Property(tpl => tpl.Id).HasColumnName("Id").IsRequired();
        builder.Property(tpl => tpl.TeacherProfileId).HasColumnName("TeacherProfileId").IsRequired();
        builder.Property(tpl => tpl.ParentProfileId).HasColumnName("ParentProfileId").IsRequired();
        builder.Property(tpl => tpl.LinkRole).HasColumnName("LinkRole").IsRequired();
        builder.Property(tpl => tpl.EffectiveFrom).HasColumnName("EffectiveFrom");
        builder.Property(tpl => tpl.EffectiveTo).HasColumnName("EffectiveTo");
        builder.Property(tpl => tpl.IsPrimary).HasColumnName("IsPrimary").IsRequired();
        builder.Property(tpl => tpl.Notes).HasColumnName("Notes");
        builder.Property(tpl => tpl.TenantId).HasColumnName("TenantId");
        builder.Property(tpl => tpl.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(tpl => tpl.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(tpl => tpl.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(tpl => !tpl.DeletedDate.HasValue);
    }
}