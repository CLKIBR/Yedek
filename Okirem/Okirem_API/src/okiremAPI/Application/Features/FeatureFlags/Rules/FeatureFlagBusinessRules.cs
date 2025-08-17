using Application.Features.FeatureFlags.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.FeatureFlags.Rules;

public class FeatureFlagBusinessRules : BaseBusinessRules
{
    private readonly IFeatureFlagRepository _featureFlagRepository;
    private readonly ILocalizationService _localizationService;

    public FeatureFlagBusinessRules(IFeatureFlagRepository featureFlagRepository, ILocalizationService localizationService)
    {
        _featureFlagRepository = featureFlagRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, FeatureFlagsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task FeatureFlagShouldExistWhenSelected(FeatureFlag? featureFlag)
    {
        if (featureFlag == null)
            await throwBusinessException(FeatureFlagsBusinessMessages.FeatureFlagNotExists);
    }

    public async Task FeatureFlagIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        FeatureFlag? featureFlag = await _featureFlagRepository.GetAsync(
            predicate: ff => ff.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FeatureFlagShouldExistWhenSelected(featureFlag);
    }
}