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

namespace Application.Features.TeacherStudentLinks.Commands.Update;

public class UpdateTeacherStudentLinkCommand : IRequest<UpdatedTeacherStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
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

    public string[] Roles => [Admin, Write, TeacherStudentLinksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherStudentLinks"];

    public class UpdateTeacherStudentLinkCommandHandler : IRequestHandler<UpdateTeacherStudentLinkCommand, UpdatedTeacherStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
        private readonly TeacherStudentLinkBusinessRules _teacherStudentLinkBusinessRules;

        public UpdateTeacherStudentLinkCommandHandler(IMapper mapper, ITeacherStudentLinkRepository teacherStudentLinkRepository,
                                         TeacherStudentLinkBusinessRules teacherStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherStudentLinkRepository = teacherStudentLinkRepository;
            _teacherStudentLinkBusinessRules = teacherStudentLinkBusinessRules;
        }

        public async Task<UpdatedTeacherStudentLinkResponse> Handle(UpdateTeacherStudentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherStudentLink? teacherStudentLink = await _teacherStudentLinkRepository.GetAsync(predicate: tsl => tsl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherStudentLinkBusinessRules.TeacherStudentLinkShouldExistWhenSelected(teacherStudentLink);
            teacherStudentLink = _mapper.Map(request, teacherStudentLink);

            await _teacherStudentLinkRepository.UpdateAsync(teacherStudentLink!);

            UpdatedTeacherStudentLinkResponse response = _mapper.Map<UpdatedTeacherStudentLinkResponse>(teacherStudentLink);
            return response;
        }
    }
}