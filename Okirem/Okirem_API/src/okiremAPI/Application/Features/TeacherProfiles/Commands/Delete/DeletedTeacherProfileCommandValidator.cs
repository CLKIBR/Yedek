using FluentValidation;

namespace Application.Features.TeacherProfiles.Commands.Delete;

public class DeleteTeacherProfileCommandValidator : AbstractValidator<DeleteTeacherProfileCommand>
{
    public DeleteTeacherProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}