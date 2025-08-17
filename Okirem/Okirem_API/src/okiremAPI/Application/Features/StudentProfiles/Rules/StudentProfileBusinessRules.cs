using Application.Features.StudentProfiles.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.StudentProfiles.Rules;

public class StudentProfileBusinessRules : BaseBusinessRules
{
    private readonly IStudentProfileRepository _studentProfileRepository;
    private readonly ILocalizationService _localizationService;

    public StudentProfileBusinessRules(IStudentProfileRepository studentProfileRepository, ILocalizationService localizationService)
    {
        _studentProfileRepository = studentProfileRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, StudentProfilesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task StudentProfileShouldExistWhenSelected(StudentProfile? studentProfile)
    {
        if (studentProfile == null)
            await throwBusinessException(StudentProfilesBusinessMessages.StudentProfileNotExists);
    }

    public async Task StudentProfileIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentProfile? studentProfile = await _studentProfileRepository.GetAsync(
            predicate: sp => sp.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentProfileShouldExistWhenSelected(studentProfile);
    }
}