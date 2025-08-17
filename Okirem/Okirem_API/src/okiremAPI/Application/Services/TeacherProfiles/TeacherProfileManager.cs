using Application.Features.TeacherProfiles.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherProfiles;

public class TeacherProfileManager : ITeacherProfileService
{
    private readonly ITeacherProfileRepository _teacherProfileRepository;
    private readonly TeacherProfileBusinessRules _teacherProfileBusinessRules;

    public TeacherProfileManager(ITeacherProfileRepository teacherProfileRepository, TeacherProfileBusinessRules teacherProfileBusinessRules)
    {
        _teacherProfileRepository = teacherProfileRepository;
        _teacherProfileBusinessRules = teacherProfileBusinessRules;
    }

    public async Task<TeacherProfile?> GetAsync(
        Expression<Func<TeacherProfile, bool>> predicate,
        Func<IQueryable<TeacherProfile>, IIncludableQueryable<TeacherProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeacherProfile? teacherProfile = await _teacherProfileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teacherProfile;
    }

    public async Task<IPaginate<TeacherProfile>?> GetListAsync(
        Expression<Func<TeacherProfile, bool>>? predicate = null,
        Func<IQueryable<TeacherProfile>, IOrderedQueryable<TeacherProfile>>? orderBy = null,
        Func<IQueryable<TeacherProfile>, IIncludableQueryable<TeacherProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeacherProfile> teacherProfileList = await _teacherProfileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teacherProfileList;
    }

    public async Task<TeacherProfile> AddAsync(TeacherProfile teacherProfile)
    {
        TeacherProfile addedTeacherProfile = await _teacherProfileRepository.AddAsync(teacherProfile);

        return addedTeacherProfile;
    }

    public async Task<TeacherProfile> UpdateAsync(TeacherProfile teacherProfile)
    {
        TeacherProfile updatedTeacherProfile = await _teacherProfileRepository.UpdateAsync(teacherProfile);

        return updatedTeacherProfile;
    }

    public async Task<TeacherProfile> DeleteAsync(TeacherProfile teacherProfile, bool permanent = false)
    {
        TeacherProfile deletedTeacherProfile = await _teacherProfileRepository.DeleteAsync(teacherProfile);

        return deletedTeacherProfile;
    }
}
