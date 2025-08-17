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

namespace Application.Features.ParentStudentLinks.Commands.Create;

public class CreateParentStudentLinkCommand : IRequest<CreatedParentStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid ParentProfileId { get; set; }
    public required Guid StudentProfileId { get; set; }
    public required RelationshipType Relationship { get; set; }
    public required bool IsPrimary { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, ParentStudentLinksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParentStudentLinks"];

    public class CreateParentStudentLinkCommandHandler : IRequestHandler<CreateParentStudentLinkCommand, CreatedParentStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
        private readonly ParentStudentLinkBusinessRules _parentStudentLinkBusinessRules;

        public CreateParentStudentLinkCommandHandler(IMapper mapper, IParentStudentLinkRepository parentStudentLinkRepository,
                                         ParentStudentLinkBusinessRules parentStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _parentStudentLinkRepository = parentStudentLinkRepository;
            _parentStudentLinkBusinessRules = parentStudentLinkBusinessRules;
        }

        public async Task<CreatedParentStudentLinkResponse> Handle(CreateParentStudentLinkCommand request, CancellationToken cancellationToken)
        {
            ParentStudentLink parentStudentLink = _mapper.Map<ParentStudentLink>(request);

            await _parentStudentLinkRepository.AddAsync(parentStudentLink);

            CreatedParentStudentLinkResponse response = _mapper.Map<CreatedParentStudentLinkResponse>(parentStudentLink);
            return response;
        }
    }
}