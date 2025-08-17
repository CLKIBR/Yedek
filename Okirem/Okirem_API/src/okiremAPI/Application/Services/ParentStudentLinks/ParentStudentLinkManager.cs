using Application.Features.ParentStudentLinks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ParentStudentLinks;

public class ParentStudentLinkManager : IParentStudentLinkService
{
    private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
    private readonly ParentStudentLinkBusinessRules _parentStudentLinkBusinessRules;

    public ParentStudentLinkManager(IParentStudentLinkRepository parentStudentLinkRepository, ParentStudentLinkBusinessRules parentStudentLinkBusinessRules)
    {
        _parentStudentLinkRepository = parentStudentLinkRepository;
        _parentStudentLinkBusinessRules = parentStudentLinkBusinessRules;
    }

    public async Task<ParentStudentLink?> GetAsync(
        Expression<Func<ParentStudentLink, bool>> predicate,
        Func<IQueryable<ParentStudentLink>, IIncludableQueryable<ParentStudentLink, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ParentStudentLink? parentStudentLink = await _parentStudentLinkRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return parentStudentLink;
    }

    public async Task<IPaginate<ParentStudentLink>?> GetListAsync(
        Expression<Func<ParentStudentLink, bool>>? predicate = null,
        Func<IQueryable<ParentStudentLink>, IOrderedQueryable<ParentStudentLink>>? orderBy = null,
        Func<IQueryable<ParentStudentLink>, IIncludableQueryable<ParentStudentLink, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ParentStudentLink> parentStudentLinkList = await _parentStudentLinkRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return parentStudentLinkList;
    }

    public async Task<ParentStudentLink> AddAsync(ParentStudentLink parentStudentLink)
    {
        ParentStudentLink addedParentStudentLink = await _parentStudentLinkRepository.AddAsync(parentStudentLink);

        return addedParentStudentLink;
    }

    public async Task<ParentStudentLink> UpdateAsync(ParentStudentLink parentStudentLink)
    {
        ParentStudentLink updatedParentStudentLink = await _parentStudentLinkRepository.UpdateAsync(parentStudentLink);

        return updatedParentStudentLink;
    }

    public async Task<ParentStudentLink> DeleteAsync(ParentStudentLink parentStudentLink, bool permanent = false)
    {
        ParentStudentLink deletedParentStudentLink = await _parentStudentLinkRepository.DeleteAsync(parentStudentLink);

        return deletedParentStudentLink;
    }
}
