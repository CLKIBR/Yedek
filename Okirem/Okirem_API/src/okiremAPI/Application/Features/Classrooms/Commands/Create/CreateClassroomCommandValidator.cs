using FluentValidation;

namespace Application.Features.Classrooms.Commands.Create;

public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
{
    public CreateClassroomCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        RuleFor(c => c.SchoolId).NotEmpty();
        RuleFor(c => c.Grade).MaximumLength(20).When(c => !string.IsNullOrEmpty(c.Grade));
        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrEmpty(c.Notes));
    }
}