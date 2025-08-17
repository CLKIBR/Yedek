using System.ComponentModel.DataAnnotations;
using NArchitecture.Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.Schools.Commands.Update;

public class UpdatedSchoolResponse : IResponse
{
    public Guid Id { get; set; }
    [Required, MaxLength(200)]
    public string Name { get; set; }
    [Required]
    public int Type { get; set; } // Enum int olarak maplenecek
    [MaxLength(100)]
    public string? City { get; set; }
    [MaxLength(100)]
    public string? District { get; set; }
    [MaxLength(100)]
    public string? Country { get; set; }
    [MaxLength(200)]
    public string? AddressLine1 { get; set; }
    [MaxLength(200)]
    public string? AddressLine2 { get; set; }
    [MaxLength(20)]
    public string? PostalCode { get; set; }
    [MaxLength(20)]
    public string? Phone { get; set; }
    [MaxLength(256)]
    public string? Email { get; set; }
    [MaxLength(256)]
    public string? WebsiteUrl { get; set; }
    [MaxLength(500)]
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }
}