using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherStudentLinks;

public interface ITeacherStudentLinkService
{
    Task<TeacherStudentLink?> GetAsync(
        Expression<Func<TeacherStudentLink, bool>> predicate,
        Func<IQueryable<TeacherStudentLink>, IIncludableQueryable<TeacherStudentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeacherStudentLink>?> GetListAsync(
        Expression<Func<TeacherStudentLink, bool>>? predicate = null,
        Func<IQueryable<TeacherStudentLink>, IOrderedQueryable<TeacherStudentLink>>? orderBy = null,
        Func<IQueryable<TeacherStudentLink>, IIncludableQueryable<TeacherStudentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeacherStudentLink> AddAsync(TeacherStudentLink teacherStudentLink);
    Task<TeacherStudentLink> UpdateAsync(TeacherStudentLink teacherStudentLink);
    Task<TeacherStudentLink> DeleteAsync(TeacherStudentLink teacherStudentLink, bool permanent = false);
}
