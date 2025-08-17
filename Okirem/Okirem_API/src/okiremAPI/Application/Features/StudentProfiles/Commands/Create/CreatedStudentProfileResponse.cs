using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.StudentProfiles.Commands.Create;

public class CreatedStudentProfileResponse : IResponse
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(256)]
    public string? Email { get; set; }
    [MaxLength(256)]
    public string? AlternateEmail { get; set; }
    [MaxLength(30)]
    public string? SchoolNumber { get; set; }
    [Required]
    public int Gender { get; set; } // Enum int olarak maplenecek
    [MaxLength(500)]
    public string? Notes { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AlternatePhoneNumber { get; set; }
    public string? Locale { get; set; }
    public bool IsActive { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public Guid? SchoolId { get; set; }
    public Guid? ClassroomId { get; set; }
    public int? GradeLevel { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public bool IsArchived { get; set; }
    public int Level { get; set; }
    public int XP { get; set; }
    public int TotalPoints { get; set; }
    public string? BadgesJson { get; set; }
    public int StreakDays { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public int CompletedTaskCount { get; set; }
    public decimal AverageScore { get; set; }
    public DateTime? LastCourseAccessAt { get; set; }
    public string? TagsJson { get; set; }
    public string? MetadataJson { get; set; }
}