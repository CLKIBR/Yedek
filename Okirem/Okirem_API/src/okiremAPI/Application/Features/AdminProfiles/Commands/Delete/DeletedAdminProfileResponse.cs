using NArchitecture.Core.Application.Responses;

namespace Application.Features.AdminProfiles.Commands.Delete;

public class DeletedAdminProfileResponse : IResponse
{
    public Guid Id { get; set; }
}