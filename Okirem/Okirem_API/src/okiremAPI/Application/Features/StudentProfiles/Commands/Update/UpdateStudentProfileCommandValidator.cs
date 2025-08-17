using FluentValidation;

namespace Application.Features.StudentProfiles.Commands.Update;

public class UpdateStudentProfileCommandValidator : AbstractValidator<UpdateStudentProfileCommand>
{
    public UpdateStudentProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Email).MaximumLength(256).EmailAddress().When(c => !string.IsNullOrEmpty(c.Email));
        RuleFor(c => c.AlternateEmail).MaximumLength(256).EmailAddress().When(c => !string.IsNullOrEmpty(c.AlternateEmail));
        RuleFor(c => c.SchoolNumber).MaximumLength(30);
        RuleFor(c => c.Gender).IsInEnum();
        RuleFor(c => c.IsActive).NotNull();
        RuleFor(c => c.IsArchived).NotNull();
        RuleFor(c => c.Level).GreaterThanOrEqualTo(0);
        RuleFor(c => c.XP).GreaterThanOrEqualTo(0);
        RuleFor(c => c.TotalPoints).GreaterThanOrEqualTo(0);
        RuleFor(c => c.StreakDays).GreaterThanOrEqualTo(0);
        RuleFor(c => c.CompletedTaskCount).GreaterThanOrEqualTo(0);
        RuleFor(c => c.AverageScore).GreaterThanOrEqualTo(0);
        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrEmpty(c.Notes));
    }
}