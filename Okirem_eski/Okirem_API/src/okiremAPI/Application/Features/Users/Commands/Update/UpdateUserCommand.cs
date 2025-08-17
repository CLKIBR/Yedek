using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Security.Hashing;
using static Application.Features.Users.Constants.UsersOperationClaims;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    // Kiþisel Bilgiler
    public string PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public GenderType Gender { get; set; }
    public string Address { get; set; }
    public string NationalId { get; set; }
    public string ProfileImageUrl { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string Institution { get; set; }
    public PositionType Position { get; set; }
    public LanguageType PreferredLanguage { get; set; }
    public string Bio { get; set; }
    public string SocialLinks { get; set; }
    // Oyunlaþtýrma Bilgileri
    public int ExperiencePoints { get; set; }
    public int Level { get; set; }
    public int BadgeCount { get; set; }
    public int Coin { get; set; }
    public int AchievementCount { get; set; }
    public int Streak { get; set; }
    public int Rank { get; set; }
    public int CompletedQuests { get; set; }
    public string CurrentQuest { get; set; }
    public double Progress { get; set; }
    public int TotalLoginCount { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsPhoneVerified { get; set; }
    public UpdateUserCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }
    public UpdateUserCommand(Guid id, string firstName, string lastName, string email, string password)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
    public string[] Roles => new[] { Admin, Write, UsersOperationClaims.Update };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);
            user = _mapper.Map(request, user);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user!.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.UpdateAsync(user);

            UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
            return response;
        }
    }
}
