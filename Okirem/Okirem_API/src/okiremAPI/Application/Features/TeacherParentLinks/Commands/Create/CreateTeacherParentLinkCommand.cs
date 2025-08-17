using Application.Features.TeacherParentLinks.Constants;
using Application.Features.TeacherParentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.TeacherParentLinks.Constants.TeacherParentLinksOperationClaims;

namespace Application.Features.TeacherParentLinks.Commands.Create;

public class CreateTeacherParentLinkCommand : IRequest<CreatedTeacherParentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid TeacherProfileId { get; set; }
    public required Guid ParentProfileId { get; set; }
    public required LinkRoleType LinkRole { get; set; }
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public required bool IsPrimary { get; set; }
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, TeacherParentLinksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherParentLinks"];

    public class CreateTeacherParentLinkCommandHandler : IRequestHandler<CreateTeacherParentLinkCommand, CreatedTeacherParentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
        private readonly TeacherParentLinkBusinessRules _teacherParentLinkBusinessRules;

        public CreateTeacherParentLinkCommandHandler(IMapper mapper, ITeacherParentLinkRepository teacherParentLinkRepository,
                                         TeacherParentLinkBusinessRules teacherParentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherParentLinkRepository = teacherParentLinkRepository;
            _teacherParentLinkBusinessRules = teacherParentLinkBusinessRules;
        }

        public async Task<CreatedTeacherParentLinkResponse> Handle(CreateTeacherParentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherParentLink teacherParentLink = _mapper.Map<TeacherParentLink>(request);

            await _teacherParentLinkRepository.AddAsync(teacherParentLink);

            CreatedTeacherParentLinkResponse response = _mapper.Map<CreatedTeacherParentLinkResponse>(teacherParentLink);
            return response;
        }
    }
}