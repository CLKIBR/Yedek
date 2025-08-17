using Application.Features.TeacherParentLinks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.TeacherParentLinks.Rules;

public class TeacherParentLinkBusinessRules : BaseBusinessRules
{
    private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
    private readonly ILocalizationService _localizationService;

    public TeacherParentLinkBusinessRules(ITeacherParentLinkRepository teacherParentLinkRepository, ILocalizationService localizationService)
    {
        _teacherParentLinkRepository = teacherParentLinkRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, TeacherParentLinksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task TeacherParentLinkShouldExistWhenSelected(TeacherParentLink? teacherParentLink)
    {
        if (teacherParentLink == null)
            await throwBusinessException(TeacherParentLinksBusinessMessages.TeacherParentLinkNotExists);
    }

    public async Task TeacherParentLinkIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        TeacherParentLink? teacherParentLink = await _teacherParentLinkRepository.GetAsync(
            predicate: tpl => tpl.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TeacherParentLinkShouldExistWhenSelected(teacherParentLink);
    }
}