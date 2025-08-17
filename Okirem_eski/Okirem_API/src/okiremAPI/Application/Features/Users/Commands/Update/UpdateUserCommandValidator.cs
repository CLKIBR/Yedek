using Domain.Enums;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using Application.Features.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private static readonly List<GenderType> AllowedGenders = new() { GenderType.NotSpecified, GenderType.Male, GenderType.Female, GenderType.Other };
    private static readonly List<PositionType> AllowedPositions = new() { PositionType.NotSpecified, PositionType.Student, PositionType.Teacher, PositionType.Admin, PositionType.Other };
    private static readonly List<LanguageType> AllowedLanguages = new() { LanguageType.NotSpecified, LanguageType.Turkish, LanguageType.English, LanguageType.German, LanguageType.French };
    public UpdateUserCommandValidator()
    {
        // Zorunlu alanlar
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
        RuleFor(c => c.Gender)
            .Must(g => AllowedGenders.Contains(g))
            .WithMessage($"Gender must be one of: {string.Join(", ", AllowedGenders)}");
        RuleFor(c => c.Position)
            .Must(p => AllowedPositions.Contains(p))
            .WithMessage($"Position must be one of: {string.Join(", ", AllowedPositions)}");
        RuleFor(c => c.PreferredLanguage)
            .Must(l => AllowedLanguages.Contains(l))
            .WithMessage($"PreferredLanguage must be one of: {string.Join(", ", AllowedLanguages)}");
        // Opsiyonel alanlar için ek validasyon istenirse eklenebilir
        // Örneğin: RuleFor(c => c.PhoneNumber).MaximumLength(20);
    }
}
