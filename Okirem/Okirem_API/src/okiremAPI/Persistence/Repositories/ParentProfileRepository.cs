using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ParentProfileRepository : EfRepositoryBase<ParentProfile, Guid, BaseDbContext>, IParentProfileRepository
{
    public ParentProfileRepository(BaseDbContext context) : base(context)
    {
    }
}