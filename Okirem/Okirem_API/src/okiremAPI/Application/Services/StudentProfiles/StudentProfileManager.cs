using Application.Features.StudentProfiles.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentProfiles;

public class StudentProfileManager : IStudentProfileService
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly StudentProfileBusinessRules _studentProfileBusinessRules;

    public StudentProfileManager(IStudentProfileRepository studentProfileRepository, StudentProfileBusinessRules studentProfileBusinessRules)
    {
        _studentProfileRepository = studentProfileRepository;
        _studentProfileBusinessRules = studentProfileBusinessRules;
    }

    public async Task<StudentProfile?> GetAsync(
        Expression<Func<StudentProfile, bool>> predicate,
        Func<IQueryable<StudentProfile>, IIncludableQueryable<StudentProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentProfile? studentProfile = await _studentProfileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentProfile;
    }

    public async Task<IPaginate<StudentProfile>?> GetListAsync(
        Expression<Func<StudentProfile, bool>>? predicate = null,
        Func<IQueryable<StudentProfile>, IOrderedQueryable<StudentProfile>>? orderBy = null,
        Func<IQueryable<StudentProfile>, IIncludableQueryable<StudentProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentProfile> studentProfileList = await _studentProfileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentProfileList;
    }

    public async Task<StudentProfile> AddAsync(StudentProfile studentProfile)
    {
        StudentProfile addedStudentProfile = await _studentProfileRepository.AddAsync(studentProfile);

        return addedStudentProfile;
    }

    public async Task<StudentProfile> UpdateAsync(StudentProfile studentProfile)
    {
        StudentProfile updatedStudentProfile = await _studentProfileRepository.UpdateAsync(studentProfile);

        return updatedStudentProfile;
    }

    public async Task<StudentProfile> DeleteAsync(StudentProfile studentProfile, bool permanent = false)
    {
        StudentProfile deletedStudentProfile = await _studentProfileRepository.DeleteAsync(studentProfile);

        return deletedStudentProfile;
    }
}
