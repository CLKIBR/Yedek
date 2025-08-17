using NArchitecture.Core.Application.Responses;

namespace Application.Features.ParentProfiles.Commands.Delete;

public class DeletedParentProfileResponse : IResponse
{
    public Guid Id { get; set; }
}