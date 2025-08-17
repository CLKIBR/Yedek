using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherProfiles;

public interface ITeacherProfileService
{
    Task<TeacherProfile?> GetAsync(
        Expression<Func<TeacherProfile, bool>> predicate,
        Func<IQueryable<TeacherProfile>, IIncludableQueryable<TeacherProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TeacherProfile>?> GetListAsync(
        Expression<Func<TeacherProfile, bool>>? predicate = null,
        Func<IQueryable<TeacherProfile>, IOrderedQueryable<TeacherProfile>>? orderBy = null,
        Func<IQueryable<TeacherProfile>, IIncludableQueryable<TeacherProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TeacherProfile> AddAsync(TeacherProfile teacherProfile);
    Task<TeacherProfile> UpdateAsync(TeacherProfile teacherProfile);
    Task<TeacherProfile> DeleteAsync(TeacherProfile teacherProfile, bool permanent = false);
}
