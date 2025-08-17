using FluentValidation;

namespace Application.Features.ParentStudentLinks.Commands.Delete;

public class DeleteParentStudentLinkCommandValidator : AbstractValidator<DeleteParentStudentLinkCommand>
{
    public DeleteParentStudentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}