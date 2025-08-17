using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
    // Kiþisel Bilgiler
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
    public string NationalId { get; set; }
    public string ProfileImageUrl { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string Institution { get; set; }
    public string Position { get; set; }
    public string PreferredLanguage { get; set; }
    public string Bio { get; set; }
    public string SocialLinks { get; set; }
    // Oyunlaþtýrma Bilgileri
    public int ExperiencePoints { get; set; }
    public int Level { get; set; }
    public int BadgeCount { get; set; }
    public int Coin { get; set; }
    public int AchievementCount { get; set; }
    public int Streak { get; set; }
    public int Rank { get; set; }
    public int CompletedQuests { get; set; }
    public string CurrentQuest { get; set; }
    public double Progress { get; set; }
    public int TotalLoginCount { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsPhoneVerified { get; set; }
    public GetByIdUserResponse() { }
    public GetByIdUserResponse(Guid id, string firstName, string lastName, string email, bool status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
    }
}
