using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IParentStudentLinkRepository : IAsyncRepository<ParentStudentLink, Guid>, IRepository<ParentStudentLink, Guid>
{
}