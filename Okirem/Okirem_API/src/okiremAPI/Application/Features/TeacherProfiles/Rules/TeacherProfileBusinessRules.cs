using Application.Features.TeacherProfiles.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.TeacherProfiles.Rules;

public class TeacherProfileBusinessRules : BaseBusinessRules
{
    private readonly ITeacherProfileRepository _teacherProfileRepository;
    private readonly ILocalizationService _localizationService;

    public TeacherProfileBusinessRules(ITeacherProfileRepository teacherProfileRepository, ILocalizationService localizationService)
    {
        _teacherProfileRepository = teacherProfileRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TeacherProfilesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TeacherProfileShouldExistWhenSelected(TeacherProfile? teacherProfile)
    {
        if (teacherProfile == null)
            await throwBusinessException(TeacherProfilesBusinessMessages.TeacherProfileNotExists);
    }

    public async Task TeacherProfileIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeacherProfile? teacherProfile = await _teacherProfileRepository.GetAsync(
            predicate: tp => tp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeacherProfileShouldExistWhenSelected(teacherProfile);
    }
}