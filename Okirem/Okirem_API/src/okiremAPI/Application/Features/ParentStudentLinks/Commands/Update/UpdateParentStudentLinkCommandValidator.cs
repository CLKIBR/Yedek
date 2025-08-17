using FluentValidation;

namespace Application.Features.ParentStudentLinks.Commands.Update;

public class UpdateParentStudentLinkCommandValidator : AbstractValidator<UpdateParentStudentLinkCommand>
{
    public UpdateParentStudentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ParentProfileId).NotEmpty();
        RuleFor(c => c.StudentProfileId).NotEmpty();
        RuleFor(c => c.Relationship).IsInEnum();
        RuleFor(c => c.IsPrimary).NotNull();
    }
}