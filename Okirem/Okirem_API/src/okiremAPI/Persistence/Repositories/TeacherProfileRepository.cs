using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TeacherProfileRepository : EfRepositoryBase<TeacherProfile, Guid, BaseDbContext>, ITeacherProfileRepository
{
    public TeacherProfileRepository(BaseDbContext context) : base(context)
    {
    }
}