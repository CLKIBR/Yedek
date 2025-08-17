using Application.Features.StudentProfiles.Constants;
using Application.Features.StudentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.StudentProfiles.Constants.StudentProfilesOperationClaims;

namespace Application.Features.StudentProfiles.Commands.Create;

public class CreateStudentProfileCommand : IRequest<CreatedStudentProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
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
    public Guid? SchoolId { get; set; }
    public Guid? ClassroomId { get; set; }
    public string? SchoolNumber { get; set; }
    public int? GradeLevel { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public required bool IsArchived { get; set; }
    public required int Level { get; set; }
    public required int XP { get; set; }
    public required int TotalPoints { get; set; }
    public string? BadgesJson { get; set; }
    public required int StreakDays { get; set; }
    public DateTime? LastActivityAt { get; set; }
    public required int CompletedTaskCount { get; set; }
    public required decimal AverageScore { get; set; }
    public DateTime? LastCourseAccessAt { get; set; }
    public string? Notes { get; set; }
    public string? TagsJson { get; set; }
    public string? MetadataJson { get; set; }

    public string[] Roles => [Admin, Write, StudentProfilesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStudentProfiles"];

    public class CreateStudentProfileCommandHandler : IRequestHandler<CreateStudentProfileCommand, CreatedStudentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentProfileRepository _studentProfileRepository;
        private readonly StudentProfileBusinessRules _studentProfileBusinessRules;

        public CreateStudentProfileCommandHandler(IMapper mapper, IStudentProfileRepository studentProfileRepository,
                                         StudentProfileBusinessRules studentProfileBusinessRules)
        {
            _mapper = mapper;
            _studentProfileRepository = studentProfileRepository;
            _studentProfileBusinessRules = studentProfileBusinessRules;
        }

        public async Task<CreatedStudentProfileResponse> Handle(CreateStudentProfileCommand request, CancellationToken cancellationToken)
        {
            StudentProfile studentProfile = _mapper.Map<StudentProfile>(request);

            await _studentProfileRepository.AddAsync(studentProfile);

            CreatedStudentProfileResponse response = _mapper.Map<CreatedStudentProfileResponse>(studentProfile);
            return response;
        }
    }
}