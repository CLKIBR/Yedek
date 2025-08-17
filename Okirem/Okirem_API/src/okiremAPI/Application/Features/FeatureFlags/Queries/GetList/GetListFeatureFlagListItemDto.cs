using NArchitecture.Core.Application.Dtos;

namespace Application.Features.FeatureFlags.Queries.GetList;

public class GetListFeatureFlagListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public string? Metadata { get; set; }
}