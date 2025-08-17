using Application.Features.AdminProfiles.Constants;
using Application.Features.AdminProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.AdminProfiles.Constants.AdminProfilesOperationClaims;

namespace Application.Features.AdminProfiles.Commands.Update;

public class UpdateAdminProfileCommand : IRequest<UpdatedAdminProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Email { get; set; }
    public string? AlternateEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AlternatePhoneNumber { get; set; }
    public string? Locale { get; set; }
    public required bool IsActive { get; set; }
    public required GenderType Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public required bool IsArchived { get; set; }
    public string? Notes { get; set; }
    public string? TagsJson { get; set; }
    public string? MetadataJson { get; set; }

    public string[] Roles => [Admin, Write, AdminProfilesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAdminProfiles"];

    public class UpdateAdminProfileCommandHandler : IRequestHandler<UpdateAdminProfileCommand, UpdatedAdminProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly AdminProfileBusinessRules _adminProfileBusinessRules;

        public UpdateAdminProfileCommandHandler(IMapper mapper, IAdminProfileRepository adminProfileRepository,
                                         AdminProfileBusinessRules adminProfileBusinessRules)
        {
            _mapper = mapper;
            _adminProfileRepository = adminProfileRepository;
            _adminProfileBusinessRules = adminProfileBusinessRules;
        }

        public async Task<UpdatedAdminProfileResponse> Handle(UpdateAdminProfileCommand request, CancellationToken cancellationToken)
        {
            AdminProfile? adminProfile = await _adminProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _adminProfileBusinessRules.AdminProfileShouldExistWhenSelected(adminProfile);
            adminProfile = _mapper.Map(request, adminProfile);

            await _adminProfileRepository.UpdateAsync(adminProfile!);

            UpdatedAdminProfileResponse response = _mapper.Map<UpdatedAdminProfileResponse>(adminProfile);
            return response;
        }
    }
}