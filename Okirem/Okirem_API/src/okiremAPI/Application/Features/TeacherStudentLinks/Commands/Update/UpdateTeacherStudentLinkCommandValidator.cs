using FluentValidation;

namespace Application.Features.TeacherStudentLinks.Commands.Update;

public class UpdateTeacherStudentLinkCommandValidator : AbstractValidator<UpdateTeacherStudentLinkCommand>
{
    public UpdateTeacherStudentLinkCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TeacherProfileId).NotEmpty();
        RuleFor(c => c.StudentProfileId).NotEmpty();
        RuleFor(c => c.LinkRole).IsInEnum();
        RuleFor(c => c.IsPrimary).NotNull();
        RuleFor(c => c.AcademicYear)
            .MaximumLength(20)
            .When(c => !string.IsNullOrEmpty(c.AcademicYear));
        RuleFor(c => c.Notes)
            .MaximumLength(500)
            .When(c => !string.IsNullOrEmpty(c.Notes));
    }
}