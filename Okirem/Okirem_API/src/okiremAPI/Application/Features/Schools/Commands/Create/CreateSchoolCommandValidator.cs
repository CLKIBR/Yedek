using FluentValidation;

namespace Application.Features.Schools.Commands.Create;

public class CreateSchoolCommandValidator : AbstractValidator<CreateSchoolCommand>
{
    public CreateSchoolCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
        RuleFor(c => c.Type).IsInEnum();
        RuleFor(c => c.Email).MaximumLength(256).EmailAddress().When(c => !string.IsNullOrEmpty(c.Email));
        RuleFor(c => c.WebsiteUrl).MaximumLength(256).When(c => !string.IsNullOrEmpty(c.WebsiteUrl));
        RuleFor(c => c.Notes).MaximumLength(500).When(c => !string.IsNullOrEmpty(c.Notes));
    }
}