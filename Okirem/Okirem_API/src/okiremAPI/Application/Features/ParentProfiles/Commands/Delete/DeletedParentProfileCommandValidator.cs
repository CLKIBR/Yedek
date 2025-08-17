using FluentValidation;

namespace Application.Features.ParentProfiles.Commands.Delete;

public class DeleteParentProfileCommandValidator : AbstractValidator<DeleteParentProfileCommand>
{
    public DeleteParentProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}