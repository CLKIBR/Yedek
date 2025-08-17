using NArchitecture.Core.Application.Responses;

namespace Application.Features.FeatureFlags.Commands.Delete;

public class DeletedFeatureFlagResponse : IResponse
{
    public Guid Id { get; set; }
}