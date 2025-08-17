using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AdminProfileRepository : EfRepositoryBase<AdminProfile, Guid, BaseDbContext>, IAdminProfileRepository
{
    public AdminProfileRepository(BaseDbContext context) : base(context)
    {
    }
}