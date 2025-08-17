using NArchitecture.Core.Application.Responses;

namespace Application.Features.TeacherProfiles.Commands.Delete;

public class DeletedTeacherProfileResponse : IResponse
{
    public Guid Id { get; set; }
}