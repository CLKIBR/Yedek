using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.TeacherParentLinks.Queries.GetList;

public class GetListTeacherParentLinkListItemDto : IDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid TeacherProfileId { get; set; }
    [Required]
    public Guid ParentProfileId { get; set; }
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