using NArchitecture.Core.Application.Responses;

namespace Application.Features.TeacherStudentLinks.Commands.Delete;

public class DeletedTeacherStudentLinkResponse : IResponse
{
    public Guid Id { get; set; }
}