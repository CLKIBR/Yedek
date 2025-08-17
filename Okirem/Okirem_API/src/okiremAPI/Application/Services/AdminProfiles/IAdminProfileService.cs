using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AdminProfiles;

public interface IAdminProfileService
{
    Task<AdminProfile?> GetAsync(
        Expression<Func<AdminProfile, bool>> predicate,
        Func<IQueryable<AdminProfile>, IIncludableQueryable<AdminProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AdminProfile>?> GetListAsync(
        Expression<Func<AdminProfile, bool>>? predicate = null,
        Func<IQueryable<AdminProfile>, IOrderedQueryable<AdminProfile>>? orderBy = null,
        Func<IQueryable<AdminProfile>, IIncludableQueryable<AdminProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AdminProfile> AddAsync(AdminProfile adminProfile);
    Task<AdminProfile> UpdateAsync(AdminProfile adminProfile);
    Task<AdminProfile> DeleteAsync(AdminProfile adminProfile, bool permanent = false);
}
