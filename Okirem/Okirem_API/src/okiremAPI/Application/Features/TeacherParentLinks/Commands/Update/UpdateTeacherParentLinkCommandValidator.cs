using FluentValidation;

namespace Application.Features.TeacherParentLinks.Commands.Update;

public class UpdateTeacherParentLinkCommandValidator : AbstractValidator<UpdateTeacherParentLinkCommand>
{
    public UpdateTeacherParentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TeacherProfileId).NotEmpty();
        RuleFor(c => c.ParentProfileId).NotEmpty();
        RuleFor(c => c.LinkRole).IsInEnum();
        RuleFor(c => c.IsPrimary).NotNull();
    }
}