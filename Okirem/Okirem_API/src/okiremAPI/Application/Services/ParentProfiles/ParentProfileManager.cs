using Application.Features.ParentProfiles.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ParentProfiles;

public class ParentProfileManager : IParentProfileService
{
    private readonly IParentProfileRepository _parentProfileRepository;
    private readonly ParentProfileBusinessRules _parentProfileBusinessRules;

    public ParentProfileManager(IParentProfileRepository parentProfileRepository, ParentProfileBusinessRules parentProfileBusinessRules)
    {
        _parentProfileRepository = parentProfileRepository;
        _parentProfileBusinessRules = parentProfileBusinessRules;
    }

    public async Task<ParentProfile?> GetAsync(
        Expression<Func<ParentProfile, bool>> predicate,
        Func<IQueryable<ParentProfile>, IIncludableQueryable<ParentProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ParentProfile? parentProfile = await _parentProfileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return parentProfile;
    }

    public async Task<IPaginate<ParentProfile>?> GetListAsync(
        Expression<Func<ParentProfile, bool>>? predicate = null,
        Func<IQueryable<ParentProfile>, IOrderedQueryable<ParentProfile>>? orderBy = null,
        Func<IQueryable<ParentProfile>, IIncludableQueryable<ParentProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ParentProfile> parentProfileList = await _parentProfileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return parentProfileList;
    }

    public async Task<ParentProfile> AddAsync(ParentProfile parentProfile)
    {
        ParentProfile addedParentProfile = await _parentProfileRepository.AddAsync(parentProfile);

        return addedParentProfile;
    }

    public async Task<ParentProfile> UpdateAsync(ParentProfile parentProfile)
    {
        ParentProfile updatedParentProfile = await _parentProfileRepository.UpdateAsync(parentProfile);

        return updatedParentProfile;
    }

    public async Task<ParentProfile> DeleteAsync(ParentProfile parentProfile, bool permanent = false)
    {
        ParentProfile deletedParentProfile = await _parentProfileRepository.DeleteAsync(parentProfile);

        return deletedParentProfile;
    }
}
