using FluentValidation;

namespace Application.Features.TeacherParentLinks.Commands.Create;

public class CreateTeacherParentLinkCommandValidator : AbstractValidator<CreateTeacherParentLinkCommand>
{
    public CreateTeacherParentLinkCommandValidator()
    {
        RuleFor(c => c.TeacherProfileId).NotEmpty();
        RuleFor(c => c.ParentProfileId).NotEmpty();
        RuleFor(c => c.LinkRole).IsInEnum();
        RuleFor(c => c.IsPrimary).NotNull();
    }
}