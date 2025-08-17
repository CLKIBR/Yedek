using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherParentLinks;

public interface ITeacherParentLinkService
{
    Task<TeacherParentLink?> GetAsync(
        Expression<Func<TeacherParentLink, bool>> predicate,
        Func<IQueryable<TeacherParentLink>, IIncludableQueryable<TeacherParentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeacherParentLink>?> GetListAsync(
        Expression<Func<TeacherParentLink, bool>>? predicate = null,
        Func<IQueryable<TeacherParentLink>, IOrderedQueryable<TeacherParentLink>>? orderBy = null,
        Func<IQueryable<TeacherParentLink>, IIncludableQueryable<TeacherParentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeacherParentLink> AddAsync(TeacherParentLink teacherParentLink);
    Task<TeacherParentLink> UpdateAsync(TeacherParentLink teacherParentLink);
    Task<TeacherParentLink> DeleteAsync(TeacherParentLink teacherParentLink, bool permanent = false);
}
