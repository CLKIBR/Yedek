using NArchitecture.Core.Application.Responses;

namespace Application.Features.ParentStudentLinks.Commands.Delete;

public class DeletedParentStudentLinkResponse : IResponse
{
    public Guid Id { get; set; }
}