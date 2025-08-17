using Application.Features.TeacherStudentLinks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherStudentLinks;

public class TeacherStudentLinkManager : ITeacherStudentLinkService
{
    private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
    private readonly TeacherStudentLinkBusinessRules _teacherStudentLinkBusinessRules;

    public TeacherStudentLinkManager(ITeacherStudentLinkRepository teacherStudentLinkRepository, TeacherStudentLinkBusinessRules teacherStudentLinkBusinessRules)
    {
        _teacherStudentLinkRepository = teacherStudentLinkRepository;
        _teacherStudentLinkBusinessRules = teacherStudentLinkBusinessRules;
    }

    public async Task<TeacherStudentLink?> GetAsync(
        Expression<Func<TeacherStudentLink, bool>> predicate,
        Func<IQueryable<TeacherStudentLink>, IIncludableQueryable<TeacherStudentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeacherStudentLink? teacherStudentLink = await _teacherStudentLinkRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teacherStudentLink;
    }

    public async Task<IPaginate<TeacherStudentLink>?> GetListAsync(
        Expression<Func<TeacherStudentLink, bool>>? predicate = null,
        Func<IQueryable<TeacherStudentLink>, IOrderedQueryable<TeacherStudentLink>>? orderBy = null,
        Func<IQueryable<TeacherStudentLink>, IIncludableQueryable<TeacherStudentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeacherStudentLink> teacherStudentLinkList = await _teacherStudentLinkRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teacherStudentLinkList;
    }

    public async Task<TeacherStudentLink> AddAsync(TeacherStudentLink teacherStudentLink)
    {
        TeacherStudentLink addedTeacherStudentLink = await _teacherStudentLinkRepository.AddAsync(teacherStudentLink);

        return addedTeacherStudentLink;
    }

    public async Task<TeacherStudentLink> UpdateAsync(TeacherStudentLink teacherStudentLink)
    {
        TeacherStudentLink updatedTeacherStudentLink = await _teacherStudentLinkRepository.UpdateAsync(teacherStudentLink);

        return updatedTeacherStudentLink;
    }

    public async Task<TeacherStudentLink> DeleteAsync(TeacherStudentLink teacherStudentLink, bool permanent = false)
    {
        TeacherStudentLink deletedTeacherStudentLink = await _teacherStudentLinkRepository.DeleteAsync(teacherStudentLink);

        return deletedTeacherStudentLink;
    }
}
