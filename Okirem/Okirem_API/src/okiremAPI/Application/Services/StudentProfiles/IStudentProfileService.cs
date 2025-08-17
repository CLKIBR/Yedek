using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentProfiles;

public interface IStudentProfileService
{
    Task<StudentProfile?> GetAsync(
        Expression<Func<StudentProfile, bool>> predicate,
        Func<IQueryable<StudentProfile>, IIncludableQueryable<StudentProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentProfile>?> GetListAsync(
        Expression<Func<StudentProfile, bool>>? predicate = null,
        Func<IQueryable<StudentProfile>, IOrderedQueryable<StudentProfile>>? orderBy = null,
        Func<IQueryable<StudentProfile>, IIncludableQueryable<StudentProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentProfile> AddAsync(StudentProfile studentProfile);
    Task<StudentProfile> UpdateAsync(StudentProfile studentProfile);
    Task<StudentProfile> DeleteAsync(StudentProfile studentProfile, bool permanent = false);
}
