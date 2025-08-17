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
using Domain.Enums;
using static Application.Features.ParentStudentLinks.Constants.ParentStudentLinksOperationClaims;

namespace Application.Features.ParentStudentLinks.Commands.Update;

public class UpdateParentStudentLinkCommand : IRequest<UpdatedParentStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid ParentProfileId { get; set; }
    public required Guid StudentProfileId { get; set; }
    public required RelationshipType Relationship { get; set; }
    public required bool IsPrimary { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, ParentStudentLinksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParentStudentLinks"];

    public class UpdateParentStudentLinkCommandHandler : IRequestHandler<UpdateParentStudentLinkCommand, UpdatedParentStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
        private readonly ParentStudentLinkBusinessRules _parentStudentLinkBusinessRules;

        public UpdateParentStudentLinkCommandHandler(IMapper mapper, IParentStudentLinkRepository parentStudentLinkRepository,
                                         ParentStudentLinkBusinessRules parentStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _parentStudentLinkRepository = parentStudentLinkRepository;
            _parentStudentLinkBusinessRules = parentStudentLinkBusinessRules;
        }

        public async Task<UpdatedParentStudentLinkResponse> Handle(UpdateParentStudentLinkCommand request, CancellationToken cancellationToken)
        {
            ParentStudentLink? parentStudentLink = await _parentStudentLinkRepository.GetAsync(predicate: psl => psl.Id == request.Id, cancellationToken: cancellationToken);
            await _parentStudentLinkBusinessRules.ParentStudentLinkShouldExistWhenSelected(parentStudentLink);
            parentStudentLink = _mapper.Map(request, parentStudentLink);

            await _parentStudentLinkRepository.UpdateAsync(parentStudentLink!);

            UpdatedParentStudentLinkResponse response = _mapper.Map<UpdatedParentStudentLinkResponse>(parentStudentLink);
            return response;
        }
    }
}