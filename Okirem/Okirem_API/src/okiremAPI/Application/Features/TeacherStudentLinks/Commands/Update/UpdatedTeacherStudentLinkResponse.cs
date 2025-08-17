using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TeacherStudentLinks.Commands.Update;

public class UpdatedTeacherStudentLinkResponse : IResponse
{
    public Guid Id { get; set; }
    [Required]
    public Guid TeacherProfileId { get; set; }
    [Required]
    public Guid StudentProfileId { get; set; }
    public Guid? ClassroomId { get; set; }
    [MaxLength(20)]
    public string? AcademicYear { get; set; }
    [Required]
    public int LinkRole { get; set; } // Enum int olarak maplenecek
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    [Required]
    public bool IsPrimary { get; set; }
    [MaxLength(500)]
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }
}