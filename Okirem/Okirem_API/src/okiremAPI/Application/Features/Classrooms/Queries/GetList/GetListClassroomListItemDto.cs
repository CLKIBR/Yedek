using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Classrooms.Queries.GetList;

public class GetListClassroomListItemDto : IDto
{
    public Guid Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public Guid SchoolId { get; set; }
    [MaxLength(20)]
    public string? Grade { get; set; }
    [MaxLength(500)]
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }
}