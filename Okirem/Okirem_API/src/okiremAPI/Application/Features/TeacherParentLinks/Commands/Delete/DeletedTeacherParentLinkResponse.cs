using NArchitecture.Core.Application.Responses;

namespace Application.Features.TeacherParentLinks.Commands.Delete;

public class DeletedTeacherParentLinkResponse : IResponse
{
    public Guid Id { get; set; }
}