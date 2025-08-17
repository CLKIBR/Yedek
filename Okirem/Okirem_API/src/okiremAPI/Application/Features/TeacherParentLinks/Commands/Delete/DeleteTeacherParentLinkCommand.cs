using Application.Features.TeacherParentLinks.Constants;
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
using static Application.Features.TeacherParentLinks.Constants.TeacherParentLinksOperationClaims;

namespace Application.Features.TeacherParentLinks.Commands.Delete;

public class DeleteTeacherParentLinkCommand : IRequest<DeletedTeacherParentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, TeacherParentLinksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherParentLinks"];

    public class DeleteTeacherParentLinkCommandHandler : IRequestHandler<DeleteTeacherParentLinkCommand, DeletedTeacherParentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
        private readonly TeacherParentLinkBusinessRules _teacherParentLinkBusinessRules;

        public DeleteTeacherParentLinkCommandHandler(IMapper mapper, ITeacherParentLinkRepository teacherParentLinkRepository,
                                         TeacherParentLinkBusinessRules teacherParentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherParentLinkRepository = teacherParentLinkRepository;
            _teacherParentLinkBusinessRules = teacherParentLinkBusinessRules;
        }

        public async Task<DeletedTeacherParentLinkResponse> Handle(DeleteTeacherParentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherParentLink? teacherParentLink = await _teacherParentLinkRepository.GetAsync(predicate: tpl => tpl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherParentLinkBusinessRules.TeacherParentLinkShouldExistWhenSelected(teacherParentLink);

            await _teacherParentLinkRepository.DeleteAsync(teacherParentLink!);

            DeletedTeacherParentLinkResponse response = _mapper.Map<DeletedTeacherParentLinkResponse>(teacherParentLink);
            return response;
        }
    }
}