using FluentValidation;

namespace Application.Features.TeacherProfiles.Commands.Create;

public class CreateTeacherProfileCommandValidator : AbstractValidator<CreateTeacherProfileCommand>
{
    public CreateTeacherProfileCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Email).MaximumLength(256).EmailAddress().When(c => !string.IsNullOrEmpty(c.Email));
        RuleFor(c => c.AlternateEmail).MaximumLength(256).EmailAddress().When(c => !string.IsNullOrEmpty(c.AlternateEmail));
        RuleFor(c => c.Gender).IsInEnum();
        RuleFor(c => c.IsActive).NotNull();
        RuleFor(c => c.IsArchived).NotNull();
        RuleFor(c => c.Branch).MaximumLength(100).When(c => !string.IsNullOrEmpty(c.Branch));
        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrEmpty(c.Notes));
    }
}