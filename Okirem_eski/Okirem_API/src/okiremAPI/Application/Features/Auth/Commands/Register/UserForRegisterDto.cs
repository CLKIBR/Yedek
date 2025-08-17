using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Auth.Commands.Register;

public class UserForRegisterDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public PositionType Position { get; set; }
    public LanguageType PreferredLanguage { get; set; }
}