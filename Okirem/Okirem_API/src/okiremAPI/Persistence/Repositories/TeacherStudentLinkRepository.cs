using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TeacherStudentLinkRepository : EfRepositoryBase<TeacherStudentLink, Guid, BaseDbContext>, ITeacherStudentLinkRepository
{
    public TeacherStudentLinkRepository(BaseDbContext context) : base(context)
    {
    }
}