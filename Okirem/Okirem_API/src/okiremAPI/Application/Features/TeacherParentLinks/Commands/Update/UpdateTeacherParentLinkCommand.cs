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

namespace Application.Features.TeacherParentLinks.Commands.Update;

public class UpdateTeacherParentLinkCommand : IRequest<UpdatedTeacherParentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid TeacherProfileId { get; set; }
    public required Guid ParentProfileId { get; set; }
    public required LinkRoleType LinkRole { get; set; }
    public DateTime? EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public required bool IsPrimary { get; set; }
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, TeacherParentLinksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherParentLinks"];

    public class UpdateTeacherParentLinkCommandHandler : IRequestHandler<UpdateTeacherParentLinkCommand, UpdatedTeacherParentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
        private readonly TeacherParentLinkBusinessRules _teacherParentLinkBusinessRules;

        public UpdateTeacherParentLinkCommandHandler(IMapper mapper, ITeacherParentLinkRepository teacherParentLinkRepository,
                                         TeacherParentLinkBusinessRules teacherParentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherParentLinkRepository = teacherParentLinkRepository;
            _teacherParentLinkBusinessRules = teacherParentLinkBusinessRules;
        }

        public async Task<UpdatedTeacherParentLinkResponse> Handle(UpdateTeacherParentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherParentLink? teacherParentLink = await _teacherParentLinkRepository.GetAsync(predicate: tpl => tpl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherParentLinkBusinessRules.TeacherParentLinkShouldExistWhenSelected(teacherParentLink);
            teacherParentLink = _mapper.Map(request, teacherParentLink);

            await _teacherParentLinkRepository.UpdateAsync(teacherParentLink!);

            UpdatedTeacherParentLinkResponse response = _mapper.Map<UpdatedTeacherParentLinkResponse>(teacherParentLink);
            return response;
        }
    }
}