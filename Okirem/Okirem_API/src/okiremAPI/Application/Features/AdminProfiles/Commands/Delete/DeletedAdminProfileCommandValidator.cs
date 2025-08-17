using FluentValidation;

namespace Application.Features.AdminProfiles.Commands.Delete;

public class DeleteAdminProfileCommandValidator : AbstractValidator<DeleteAdminProfileCommand>
{
    public DeleteAdminProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}