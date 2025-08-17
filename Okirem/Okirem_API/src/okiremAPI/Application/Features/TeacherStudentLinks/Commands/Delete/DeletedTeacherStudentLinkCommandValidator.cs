using FluentValidation;

namespace Application.Features.TeacherStudentLinks.Commands.Delete;

public class DeleteTeacherStudentLinkCommandValidator : AbstractValidator<DeleteTeacherStudentLinkCommand>
{
    public DeleteTeacherStudentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}