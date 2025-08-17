using Application.Features.TeacherStudentLinks.Constants;
using Application.Features.TeacherStudentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.TeacherStudentLinks.Constants.TeacherStudentLinksOperationClaims;

namespace Application.Features.TeacherStudentLinks.Commands.Create;

public class CreateTeacherStudentLinkCommand : IRequest<CreatedTeacherStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid TeacherProfileId { get; set; }
    public required Guid StudentProfileId { get; set; }
    public Guid? ClassroomId { get; set; }
    public string? AcademicYear { get; set; }
    public required LinkRoleType LinkRole { get; set; }
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public required bool IsPrimary { get; set; }
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, TeacherStudentLinksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherStudentLinks"];

    public class CreateTeacherStudentLinkCommandHandler : IRequestHandler<CreateTeacherStudentLinkCommand, CreatedTeacherStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
        private readonly TeacherStudentLinkBusinessRules _teacherStudentLinkBusinessRules;

        public CreateTeacherStudentLinkCommandHandler(IMapper mapper, ITeacherStudentLinkRepository teacherStudentLinkRepository,
                                         TeacherStudentLinkBusinessRules teacherStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherStudentLinkRepository = teacherStudentLinkRepository;
            _teacherStudentLinkBusinessRules = teacherStudentLinkBusinessRules;
        }

        public async Task<CreatedTeacherStudentLinkResponse> Handle(CreateTeacherStudentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherStudentLink teacherStudentLink = _mapper.Map<TeacherStudentLink>(request);

            await _teacherStudentLinkRepository.AddAsync(teacherStudentLink);

            CreatedTeacherStudentLinkResponse response = _mapper.Map<CreatedTeacherStudentLinkResponse>(teacherStudentLink);
            return response;
        }
    }
}