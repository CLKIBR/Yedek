using Application.Features.FeatureFlags.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FeatureFlags;

public class FeatureFlagManager : IFeatureFlagService
{
    private readonly IFeatureFlagRepository _featureFlagRepository;
    private readonly FeatureFlagBusinessRules _featureFlagBusinessRules;

    public FeatureFlagManager(IFeatureFlagRepository featureFlagRepository, FeatureFlagBusinessRules featureFlagBusinessRules)
    {
        _featureFlagRepository = featureFlagRepository;
        _featureFlagBusinessRules = featureFlagBusinessRules;
    }

    public async Task<FeatureFlag?> GetAsync(
        Expression<Func<FeatureFlag, bool>> predicate,
        Func<IQueryable<FeatureFlag>, IIncludableQueryable<FeatureFlag, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FeatureFlag? featureFlag = await _featureFlagRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return featureFlag;
    }

    public async Task<IPaginate<FeatureFlag>?> GetListAsync(
        Expression<Func<FeatureFlag, bool>>? predicate = null,
        Func<IQueryable<FeatureFlag>, IOrderedQueryable<FeatureFlag>>? orderBy = null,
        Func<IQueryable<FeatureFlag>, IIncludableQueryable<FeatureFlag, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FeatureFlag> featureFlagList = await _featureFlagRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return featureFlagList;
    }

    public async Task<FeatureFlag> AddAsync(FeatureFlag featureFlag)
    {
        FeatureFlag addedFeatureFlag = await _featureFlagRepository.AddAsync(featureFlag);

        return addedFeatureFlag;
    }

    public async Task<FeatureFlag> UpdateAsync(FeatureFlag featureFlag)
    {
        FeatureFlag updatedFeatureFlag = await _featureFlagRepository.UpdateAsync(featureFlag);

        return updatedFeatureFlag;
    }

    public async Task<FeatureFlag> DeleteAsync(FeatureFlag featureFlag, bool permanent = false)
    {
        FeatureFlag deletedFeatureFlag = await _featureFlagRepository.DeleteAsync(featureFlag);

        return deletedFeatureFlag;
    }
}
