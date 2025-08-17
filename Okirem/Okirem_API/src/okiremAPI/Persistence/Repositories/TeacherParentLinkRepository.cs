using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TeacherParentLinkRepository : EfRepositoryBase<TeacherParentLink, Guid, BaseDbContext>, ITeacherParentLinkRepository
{
    public TeacherParentLinkRepository(BaseDbContext context) : base(context)
    {
    }
}