using FluentValidation;

namespace Application.Features.FeatureFlags.Commands.Create;

public class CreateFeatureFlagCommandValidator : AbstractValidator<CreateFeatureFlagCommand>
{
    public CreateFeatureFlagCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Enabled).NotEmpty();
    }
}