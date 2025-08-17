using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FeatureFlagRepository : EfRepositoryBase<FeatureFlag, Guid, BaseDbContext>, IFeatureFlagRepository
{
    public FeatureFlagRepository(BaseDbContext context) : base(context)
    {
    }
}