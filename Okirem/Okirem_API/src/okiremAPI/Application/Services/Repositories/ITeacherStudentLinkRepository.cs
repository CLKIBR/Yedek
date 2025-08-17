using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeacherStudentLinkRepository : IAsyncRepository<TeacherStudentLink, Guid>, IRepository<TeacherStudentLink, Guid>
{
}