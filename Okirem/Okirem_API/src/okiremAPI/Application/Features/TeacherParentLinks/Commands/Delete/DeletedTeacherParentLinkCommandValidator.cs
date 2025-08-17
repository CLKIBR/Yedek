using FluentValidation;

namespace Application.Features.TeacherParentLinks.Commands.Delete;

public class DeleteTeacherParentLinkCommandValidator : AbstractValidator<DeleteTeacherParentLinkCommand>
{
    public DeleteTeacherParentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}