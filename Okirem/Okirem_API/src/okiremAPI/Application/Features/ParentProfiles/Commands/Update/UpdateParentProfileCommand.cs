using Application.Features.ParentProfiles.Constants;
using Application.Features.ParentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.ParentProfiles.Constants.ParentProfilesOperationClaims;

namespace Application.Features.ParentProfiles.Commands.Update;

public class UpdateParentProfileCommand : IRequest<UpdatedParentProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
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

    public string[] Roles => [Admin, Write, ParentProfilesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParentProfiles"];

    public class UpdateParentProfileCommandHandler : IRequestHandler<UpdateParentProfileCommand, UpdatedParentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentProfileRepository _parentProfileRepository;
        private readonly ParentProfileBusinessRules _parentProfileBusinessRules;

        public UpdateParentProfileCommandHandler(IMapper mapper, IParentProfileRepository parentProfileRepository,
                                         ParentProfileBusinessRules parentProfileBusinessRules)
        {
            _mapper = mapper;
            _parentProfileRepository = parentProfileRepository;
            _parentProfileBusinessRules = parentProfileBusinessRules;
        }

        public async Task<UpdatedParentProfileResponse> Handle(UpdateParentProfileCommand request, CancellationToken cancellationToken)
        {
            ParentProfile? parentProfile = await _parentProfileRepository.GetAsync(predicate: pp => pp.Id == request.Id, cancellationToken: cancellationToken);
            await _parentProfileBusinessRules.ParentProfileShouldExistWhenSelected(parentProfile);
            parentProfile = _mapper.Map(request, parentProfile);

            await _parentProfileRepository.UpdateAsync(parentProfile!);

            UpdatedParentProfileResponse response = _mapper.Map<UpdatedParentProfileResponse>(parentProfile);
            return response;
        }
    }
}