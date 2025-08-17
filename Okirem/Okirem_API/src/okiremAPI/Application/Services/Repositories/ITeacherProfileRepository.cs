using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITeacherProfileRepository : IAsyncRepository<TeacherProfile, Guid>, IRepository<TeacherProfile, Guid>
{
}