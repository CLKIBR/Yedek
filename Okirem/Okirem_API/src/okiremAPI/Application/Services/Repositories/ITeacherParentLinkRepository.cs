using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeacherParentLinkRepository : IAsyncRepository<TeacherParentLink, Guid>, IRepository<TeacherParentLink, Guid>
{
}