using NArchitecture.Core.Application.Responses;

namespace Application.Features.Schools.Commands.Delete;

public class DeletedSchoolResponse : IResponse
{
    public Guid Id { get; set; }
}