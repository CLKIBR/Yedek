using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.ParentStudentLinks.Queries.GetList;

public class GetListParentStudentLinkListItemDto : IDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid ParentProfileId { get; set; }
    [Required]
    public Guid StudentProfileId { get; set; }
    [Required]
    public int Relationship { get; set; } // Enum int olarak maplenecek
    [Required]
    public bool IsPrimary { get; set; }
    public Guid? TenantId { get; set; }
}