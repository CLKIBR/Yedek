using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ParentProfiles;

public interface IParentProfileService
{
    Task<ParentProfile?> GetAsync(
        Expression<Func<ParentProfile, bool>> predicate,
        Func<IQueryable<ParentProfile>, IIncludableQueryable<ParentProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ParentProfile>?> GetListAsync(
        Expression<Func<ParentProfile, bool>>? predicate = null,
        Func<IQueryable<ParentProfile>, IOrderedQueryable<ParentProfile>>? orderBy = null,
        Func<IQueryable<ParentProfile>, IIncludableQueryable<ParentProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ParentProfile> AddAsync(ParentProfile parentProfile);
    Task<ParentProfile> UpdateAsync(ParentProfile parentProfile);
    Task<ParentProfile> DeleteAsync(ParentProfile parentProfile, bool permanent = false);
}
