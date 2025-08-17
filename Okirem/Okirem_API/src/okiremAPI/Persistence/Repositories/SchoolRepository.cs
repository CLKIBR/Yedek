using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SchoolRepository : EfRepositoryBase<School, Guid, BaseDbContext>, ISchoolRepository
{
    public SchoolRepository(BaseDbContext context) : base(context)
    {
    }
}