using Application.Features.ParentStudentLinks.Constants;
using Application.Features.ParentStudentLinks.Constants;
using Application.Features.ParentStudentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ParentStudentLinks.Constants.ParentStudentLinksOperationClaims;

namespace Application.Features.ParentStudentLinks.Commands.Delete;

public class DeleteParentStudentLinkCommand : IRequest<DeletedParentStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ParentStudentLinksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParentStudentLinks"];

    public class DeleteParentStudentLinkCommandHandler : IRequestHandler<DeleteParentStudentLinkCommand, DeletedParentStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
        private readonly ParentStudentLinkBusinessRules _parentStudentLinkBusinessRules;

        public DeleteParentStudentLinkCommandHandler(IMapper mapper, IParentStudentLinkRepository parentStudentLinkRepository,
                                         ParentStudentLinkBusinessRules parentStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _parentStudentLinkRepository = parentStudentLinkRepository;
            _parentStudentLinkBusinessRules = parentStudentLinkBusinessRules;
        }

        public async Task<DeletedParentStudentLinkResponse> Handle(DeleteParentStudentLinkCommand request, CancellationToken cancellationToken)
        {
            ParentStudentLink? parentStudentLink = await _parentStudentLinkRepository.GetAsync(predicate: psl => psl.Id == request.Id, cancellationToken: cancellationToken);
            await _parentStudentLinkBusinessRules.ParentStudentLinkShouldExistWhenSelected(parentStudentLink);

            await _parentStudentLinkRepository.DeleteAsync(parentStudentLink!);

            DeletedParentStudentLinkResponse response = _mapper.Map<DeletedParentStudentLinkResponse>(parentStudentLink);
            return response;
        }
    }
}