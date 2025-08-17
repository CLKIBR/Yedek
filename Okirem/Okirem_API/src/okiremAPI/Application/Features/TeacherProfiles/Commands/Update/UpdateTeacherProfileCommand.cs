using Application.Features.TeacherProfiles.Constants;
using Application.Features.TeacherProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.TeacherProfiles.Constants.TeacherProfilesOperationClaims;

namespace Application.Features.TeacherProfiles.Commands.Update;

public class UpdateTeacherProfileCommand : IRequest<UpdatedTeacherProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
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
    public string? Branch { get; set; }
    public Guid? SchoolId { get; set; }
    public required bool IsArchived { get; set; }
    public string? Notes { get; set; }
    public string? TagsJson { get; set; }
    public string? MetadataJson { get; set; }

    public string[] Roles => [Admin, Write, TeacherProfilesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherProfiles"];

    public class UpdateTeacherProfileCommandHandler : IRequestHandler<UpdateTeacherProfileCommand, UpdatedTeacherProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherProfileRepository _teacherProfileRepository;
        private readonly TeacherProfileBusinessRules _teacherProfileBusinessRules;

        public UpdateTeacherProfileCommandHandler(IMapper mapper, ITeacherProfileRepository teacherProfileRepository,
                                         TeacherProfileBusinessRules teacherProfileBusinessRules)
        {
            _mapper = mapper;
            _teacherProfileRepository = teacherProfileRepository;
            _teacherProfileBusinessRules = teacherProfileBusinessRules;
        }

        public async Task<UpdatedTeacherProfileResponse> Handle(UpdateTeacherProfileCommand request, CancellationToken cancellationToken)
        {
            TeacherProfile? teacherProfile = await _teacherProfileRepository.GetAsync(predicate: tp => tp.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherProfileBusinessRules.TeacherProfileShouldExistWhenSelected(teacherProfile);
            teacherProfile = _mapper.Map(request, teacherProfile);

            await _teacherProfileRepository.UpdateAsync(teacherProfile!);

            UpdatedTeacherProfileResponse response = _mapper.Map<UpdatedTeacherProfileResponse>(teacherProfile);
            return response;
        }
    }
}