using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ParentStudentLinks;

public interface IParentStudentLinkService
{
    Task<ParentStudentLink?> GetAsync(
        Expression<Func<ParentStudentLink, bool>> predicate,
        Func<IQueryable<ParentStudentLink>, IIncludableQueryable<ParentStudentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ParentStudentLink>?> GetListAsync(
        Expression<Func<ParentStudentLink, bool>>? predicate = null,
        Func<IQueryable<ParentStudentLink>, IOrderedQueryable<ParentStudentLink>>? orderBy = null,
        Func<IQueryable<ParentStudentLink>, IIncludableQueryable<ParentStudentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ParentStudentLink> AddAsync(ParentStudentLink parentStudentLink);
    Task<ParentStudentLink> UpdateAsync(ParentStudentLink parentStudentLink);
    Task<ParentStudentLink> DeleteAsync(ParentStudentLink parentStudentLink, bool permanent = false);
}
