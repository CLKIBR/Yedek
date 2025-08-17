using Application.Features.AdminProfiles.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AdminProfiles;

public class AdminProfileManager : IAdminProfileService
{
    private readonly IAdminProfileRepository _adminProfileRepository;
    private readonly AdminProfileBusinessRules _adminProfileBusinessRules;

    public AdminProfileManager(IAdminProfileRepository adminProfileRepository, AdminProfileBusinessRules adminProfileBusinessRules)
    {
        _adminProfileRepository = adminProfileRepository;
        _adminProfileBusinessRules = adminProfileBusinessRules;
    }

    public async Task<AdminProfile?> GetAsync(
        Expression<Func<AdminProfile, bool>> predicate,
        Func<IQueryable<AdminProfile>, IIncludableQueryable<AdminProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AdminProfile? adminProfile = await _adminProfileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return adminProfile;
    }

    public async Task<IPaginate<AdminProfile>?> GetListAsync(
        Expression<Func<AdminProfile, bool>>? predicate = null,
        Func<IQueryable<AdminProfile>, IOrderedQueryable<AdminProfile>>? orderBy = null,
        Func<IQueryable<AdminProfile>, IIncludableQueryable<AdminProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AdminProfile> adminProfileList = await _adminProfileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return adminProfileList;
    }

    public async Task<AdminProfile> AddAsync(AdminProfile adminProfile)
    {
        AdminProfile addedAdminProfile = await _adminProfileRepository.AddAsync(adminProfile);

        return addedAdminProfile;
    }

    public async Task<AdminProfile> UpdateAsync(AdminProfile adminProfile)
    {
        AdminProfile updatedAdminProfile = await _adminProfileRepository.UpdateAsync(adminProfile);

        return updatedAdminProfile;
    }

    public async Task<AdminProfile> DeleteAsync(AdminProfile adminProfile, bool permanent = false)
    {
        AdminProfile deletedAdminProfile = await _adminProfileRepository.DeleteAsync(adminProfile);

        return deletedAdminProfile;
    }
}
