using Application.Features.TeacherStudentLinks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.TeacherStudentLinks.Rules;

public class TeacherStudentLinkBusinessRules : BaseBusinessRules
{
    private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
    private readonly ILocalizationService _localizationService;

    public TeacherStudentLinkBusinessRules(ITeacherStudentLinkRepository teacherStudentLinkRepository, ILocalizationService localizationService)
    {
        _teacherStudentLinkRepository = teacherStudentLinkRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TeacherStudentLinksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TeacherStudentLinkShouldExistWhenSelected(TeacherStudentLink? teacherStudentLink)
    {
        if (teacherStudentLink == null)
            await throwBusinessException(TeacherStudentLinksBusinessMessages.TeacherStudentLinkNotExists);
    }

    public async Task TeacherStudentLinkIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeacherStudentLink? teacherStudentLink = await _teacherStudentLinkRepository.GetAsync(
            predicate: tsl => tsl.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeacherStudentLinkShouldExistWhenSelected(teacherStudentLink);
    }
}