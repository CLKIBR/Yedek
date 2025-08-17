using FluentValidation;

namespace Application.Features.ParentStudentLinks.Commands.Create;

public class CreateParentStudentLinkCommandValidator : AbstractValidator<CreateParentStudentLinkCommand>
{
    public CreateParentStudentLinkCommandValidator()
    {
        RuleFor(c => c.ParentProfileId).NotEmpty();
        RuleFor(c => c.StudentProfileId).NotEmpty();
        RuleFor(c => c.Relationship).IsInEnum();
        RuleFor(c => c.IsPrimary).NotNull();
    }
}