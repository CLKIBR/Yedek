using NArchitecture.Core.Application.Responses;

namespace Application.Features.FeatureFlags.Commands.Create;

public class CreatedFeatureFlagResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public string? Metadata { get; set; }
}