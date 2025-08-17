using FluentValidation;

namespace Application.Features.FeatureFlags.Commands.Delete;

public class DeleteFeatureFlagCommandValidator : AbstractValidator<DeleteFeatureFlagCommand>
{
    public DeleteFeatureFlagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}