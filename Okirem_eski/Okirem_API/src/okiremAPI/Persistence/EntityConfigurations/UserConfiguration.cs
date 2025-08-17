using Domain.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Hashing;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        // Kişisel Bilgiler
        builder.Property(u => u.FirstName).HasColumnName("FirstName");
        builder.Property(u => u.LastName).HasColumnName("LastName");
        builder.Property(u => u.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(u => u.BirthDate).HasColumnName("BirthDate");
        builder.Property(u => u.Gender).HasColumnName("Gender");
        builder.Property(u => u.Address).HasColumnName("Address");
        builder.Property(u => u.NationalId).HasColumnName("NationalId");
        builder.Property(u => u.ProfileImageUrl).HasColumnName("ProfileImageUrl");
        builder.Property(u => u.RegistrationDate).HasColumnName("RegistrationDate");
        builder.Property(u => u.IsActive).HasColumnName("IsActive");
        builder.Property(u => u.LastLoginDate).HasColumnName("LastLoginDate");
        builder.Property(u => u.Institution).HasColumnName("Institution");
        builder.Property(u => u.Position).HasColumnName("Position");
        builder.Property(u => u.PreferredLanguage).HasColumnName("PreferredLanguage");
        builder.Property(u => u.Bio).HasColumnName("Bio");
        builder.Property(u => u.SocialLinks).HasColumnName("SocialLinks");

        // Oyunlaştırma Bilgileri
        builder.Property(u => u.ExperiencePoints).HasColumnName("ExperiencePoints");
        builder.Property(u => u.Level).HasColumnName("Level");
        builder.Property(u => u.BadgeCount).HasColumnName("BadgeCount");
        builder.Property(u => u.Coin).HasColumnName("Coin");
        builder.Property(u => u.AchievementCount).HasColumnName("AchievementCount");
        builder.Property(u => u.Streak).HasColumnName("Streak");
        builder.Property(u => u.Rank).HasColumnName("Rank");
        builder.Property(u => u.CompletedQuests).HasColumnName("CompletedQuests");
        builder.Property(u => u.CurrentQuest).HasColumnName("CurrentQuest");
        builder.Property(u => u.Progress).HasColumnName("Progress");
        builder.Property(u => u.TotalLoginCount).HasColumnName("TotalLoginCount");
        builder.Property(u => u.IsEmailVerified).HasColumnName("IsEmailVerified");
        builder.Property(u => u.IsPhoneVerified).HasColumnName("IsPhoneVerified");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserOperationClaims);
        builder.HasMany(u => u.RefreshTokens);
        builder.HasMany(u => u.EmailAuthenticators);
        builder.HasMany(u => u.OtpAuthenticators);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "Umut1357",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    Id = AdminId,
                    Email = "clkibrahim@outlook.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = "İbrahim",
                    LastName = "Çelik",
                    PhoneNumber = "05305618806",
                    BirthDate = null,
                    Gender = GenderType.NotSpecified,
                    Address = "",
                    NationalId = "",
                    ProfileImageUrl = "",
                    RegistrationDate = DateTime.UtcNow,
                    IsActive = true,
                    LastLoginDate = null,
                    Institution = "",
                    Position = PositionType.NotSpecified,
                    PreferredLanguage = LanguageType.Turkish,
                    Bio = "",
                    SocialLinks = "",
                    ExperiencePoints = 0,
                    Level = 1,
                    BadgeCount = 0,
                    Coin = 0,
                    AchievementCount = 0,
                    Streak = 0,
                    Rank = 0,
                    CompletedQuests = 0,
                    CurrentQuest = "",
                    Progress = 0,
                    TotalLoginCount = 0,
                    IsEmailVerified = false,
                    IsPhoneVerified = false
                };
            yield return adminUser;
        }
    }
}
