using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.AdminProfiles.Constants;
using Application.Features.Classrooms.Constants;
using Application.Features.ParentProfiles.Constants;
using Application.Features.ParentStudentLinks.Constants;
using Application.Features.Schools.Constants;
using Application.Features.StudentProfiles.Constants;
using Application.Features.TeacherParentLinks.Constants;
using Application.Features.TeacherProfiles.Constants;
using Application.Features.TeacherStudentLinks.Constants;
using Application.Features.FeatureFlags.Constants;











namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region AdminProfiles CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Admin },
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Read },
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Write },
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Create },
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Update },
                new() { Id = ++lastId, Name = AdminProfilesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Classrooms CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Read },
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Write },
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Create },
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Update },
                new() { Id = ++lastId, Name = ClassroomsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ParentProfiles CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Read },
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Write },
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Create },
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Update },
                new() { Id = ++lastId, Name = ParentProfilesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ParentStudentLinks CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Admin },
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Read },
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Write },
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Create },
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Update },
                new() { Id = ++lastId, Name = ParentStudentLinksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Schools CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Admin },
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Read },
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Write },
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Create },
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Update },
                new() { Id = ++lastId, Name = SchoolsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region StudentProfiles CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Admin },
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Read },
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Write },
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Create },
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Update },
                new() { Id = ++lastId, Name = StudentProfilesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region TeacherParentLinks CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Admin },
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Read },
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Write },
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Create },
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Update },
                new() { Id = ++lastId, Name = TeacherParentLinksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region TeacherProfiles CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Admin },
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Read },
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Write },
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Create },
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Update },
                new() { Id = ++lastId, Name = TeacherProfilesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region TeacherStudentLinks CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Admin },
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Read },
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Write },
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Create },
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Update },
                new() { Id = ++lastId, Name = TeacherStudentLinksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region FeatureFlags CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Admin },
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Read },
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Write },
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Create },
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Update },
                new() { Id = ++lastId, Name = FeatureFlagsOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
