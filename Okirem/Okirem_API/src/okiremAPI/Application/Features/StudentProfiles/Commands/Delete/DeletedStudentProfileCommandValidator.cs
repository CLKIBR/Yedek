using FluentValidation;

namespace Application.Features.StudentProfiles.Commands.Delete;

public class DeleteStudentProfileCommandValidator : AbstractValidator<DeleteStudentProfileCommand>
{
    public DeleteStudentProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}