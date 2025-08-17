using Application.Features.AdminProfiles.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.AdminProfiles.Rules;

public class AdminProfileBusinessRules : BaseBusinessRules
{
    private readonly IAdminProfileRepository _adminProfileRepository;
    private readonly ILocalizationService _localizationService;

    public AdminProfileBusinessRules(IAdminProfileRepository adminProfileRepository, ILocalizationService localizationService)
    {
        _adminProfileRepository = adminProfileRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AdminProfilesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AdminProfileShouldExistWhenSelected(AdminProfile? adminProfile)
    {
        if (adminProfile == null)
            await throwBusinessException(AdminProfilesBusinessMessages.AdminProfileNotExists);
    }

    public async Task AdminProfileIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AdminProfile? adminProfile = await _adminProfileRepository.GetAsync(
            predicate: ap => ap.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AdminProfileShouldExistWhenSelected(adminProfile);
    }
}