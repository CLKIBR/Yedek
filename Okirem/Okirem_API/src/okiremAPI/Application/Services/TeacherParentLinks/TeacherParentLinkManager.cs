using Application.Features.TeacherParentLinks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TeacherParentLinks;

public class TeacherParentLinkManager : ITeacherParentLinkService
{
    private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
    private readonly TeacherParentLinkBusinessRules _teacherParentLinkBusinessRules;

    public TeacherParentLinkManager(ITeacherParentLinkRepository teacherParentLinkRepository, TeacherParentLinkBusinessRules teacherParentLinkBusinessRules)
    {
        _teacherParentLinkRepository = teacherParentLinkRepository;
        _teacherParentLinkBusinessRules = teacherParentLinkBusinessRules;
    }

    public async Task<TeacherParentLink?> GetAsync(
        Expression<Func<TeacherParentLink, bool>> predicate,
        Func<IQueryable<TeacherParentLink>, IIncludableQueryable<TeacherParentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TeacherParentLink? teacherParentLink = await _teacherParentLinkRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return teacherParentLink;
    }

    public async Task<IPaginate<TeacherParentLink>?> GetListAsync(
        Expression<Func<TeacherParentLink, bool>>? predicate = null,
        Func<IQueryable<TeacherParentLink>, IOrderedQueryable<TeacherParentLink>>? orderBy = null,
        Func<IQueryable<TeacherParentLink>, IIncludableQueryable<TeacherParentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TeacherParentLink> teacherParentLinkList = await _teacherParentLinkRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return teacherParentLinkList;
    }

    public async Task<TeacherParentLink> AddAsync(TeacherParentLink teacherParentLink)
    {
        TeacherParentLink addedTeacherParentLink = await _teacherParentLinkRepository.AddAsync(teacherParentLink);

        return addedTeacherParentLink;
    }

    public async Task<TeacherParentLink> UpdateAsync(TeacherParentLink teacherParentLink)
    {
        TeacherParentLink updatedTeacherParentLink = await _teacherParentLinkRepository.UpdateAsync(teacherParentLink);

        return updatedTeacherParentLink;
    }

    public async Task<TeacherParentLink> DeleteAsync(TeacherParentLink teacherParentLink, bool permanent = false)
    {
        TeacherParentLink deletedTeacherParentLink = await _teacherParentLinkRepository.DeleteAsync(teacherParentLink);

        return deletedTeacherParentLink;
    }
}
