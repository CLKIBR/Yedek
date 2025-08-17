using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ParentStudentLinkRepository : EfRepositoryBase<ParentStudentLink, Guid, BaseDbContext>, IParentStudentLinkRepository
{
    public ParentStudentLinkRepository(BaseDbContext context) : base(context)
    {
    }
}