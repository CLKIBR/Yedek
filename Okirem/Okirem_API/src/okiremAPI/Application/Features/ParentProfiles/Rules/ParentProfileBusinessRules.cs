using Application.Features.ParentProfiles.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ParentProfiles.Rules;

public class ParentProfileBusinessRules : BaseBusinessRules
{
    private readonly IParentProfileRepository _parentProfileRepository;
    private readonly ILocalizationService _localizationService;

    public ParentProfileBusinessRules(IParentProfileRepository parentProfileRepository, ILocalizationService localizationService)
    {
        _parentProfileRepository = parentProfileRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ParentProfilesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ParentProfileShouldExistWhenSelected(ParentProfile? parentProfile)
    {
        if (parentProfile == null)
            await throwBusinessException(ParentProfilesBusinessMessages.ParentProfileNotExists);
    }

    public async Task ParentProfileIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ParentProfile? parentProfile = await _parentProfileRepository.GetAsync(
            predicate: pp => pp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ParentProfileShouldExistWhenSelected(parentProfile);
    }
}