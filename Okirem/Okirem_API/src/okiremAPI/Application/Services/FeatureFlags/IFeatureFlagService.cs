using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeatureFlags;

public interface IFeatureFlagService
{
    Task<FeatureFlag?> GetAsync(
        Expression<Func<FeatureFlag, bool>> predicate,
        Func<IQueryable<FeatureFlag>, IIncludableQueryable<FeatureFlag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FeatureFlag>?> GetListAsync(
        Expression<Func<FeatureFlag, bool>>? predicate = null,
        Func<IQueryable<FeatureFlag>, IOrderedQueryable<FeatureFlag>>? orderBy = null,
        Func<IQueryable<FeatureFlag>, IIncludableQueryable<FeatureFlag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<FeatureFlag> AddAsync(FeatureFlag featureFlag);
    Task<FeatureFlag> UpdateAsync(FeatureFlag featureFlag);
    Task<FeatureFlag> DeleteAsync(FeatureFlag featureFlag, bool permanent = false);
}
