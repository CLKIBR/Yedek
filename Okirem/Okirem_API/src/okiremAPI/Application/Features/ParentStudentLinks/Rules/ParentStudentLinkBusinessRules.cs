using Application.Features.ParentStudentLinks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ParentStudentLinks.Rules;

public class ParentStudentLinkBusinessRules : BaseBusinessRules
{
    private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
    private readonly ILocalizationService _localizationService;

    public ParentStudentLinkBusinessRules(IParentStudentLinkRepository parentStudentLinkRepository, ILocalizationService localizationService)
    {
        _parentStudentLinkRepository = parentStudentLinkRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ParentStudentLinksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ParentStudentLinkShouldExistWhenSelected(ParentStudentLink? parentStudentLink)
    {
        if (parentStudentLink == null)
            await throwBusinessException(ParentStudentLinksBusinessMessages.ParentStudentLinkNotExists);
    }

    public async Task ParentStudentLinkIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ParentStudentLink? parentStudentLink = await _parentStudentLinkRepository.GetAsync(
            predicate: psl => psl.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ParentStudentLinkShouldExistWhenSelected(parentStudentLink);
    }
}