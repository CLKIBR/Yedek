using NArchitecture.Core.Application.Responses;

namespace Application.Features.StudentProfiles.Commands.Delete;

public class DeletedStudentProfileResponse : IResponse
{
    public Guid Id { get; set; }
}