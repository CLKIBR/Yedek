using Application.Features.ParentProfiles.Constants;
using Application.Features.ParentProfiles.Constants;
using Application.Features.ParentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ParentProfiles.Constants.ParentProfilesOperationClaims;

namespace Application.Features.ParentProfiles.Commands.Delete;

public class DeleteParentProfileCommand : IRequest<DeletedParentProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ParentProfilesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParentProfiles"];

    public class DeleteParentProfileCommandHandler : IRequestHandler<DeleteParentProfileCommand, DeletedParentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentProfileRepository _parentProfileRepository;
        private readonly ParentProfileBusinessRules _parentProfileBusinessRules;

        public DeleteParentProfileCommandHandler(IMapper mapper, IParentProfileRepository parentProfileRepository,
                                         ParentProfileBusinessRules parentProfileBusinessRules)
        {
            _mapper = mapper;
            _parentProfileRepository = parentProfileRepository;
            _parentProfileBusinessRules = parentProfileBusinessRules;
        }

        public async Task<DeletedParentProfileResponse> Handle(DeleteParentProfileCommand request, CancellationToken cancellationToken)
        {
            ParentProfile? parentProfile = await _parentProfileRepository.GetAsync(predicate: pp => pp.Id == request.Id, cancellationToken: cancellationToken);
            await _parentProfileBusinessRules.ParentProfileShouldExistWhenSelected(parentProfile);

            await _parentProfileRepository.DeleteAsync(parentProfile!);

            DeletedParentProfileResponse response = _mapper.Map<DeletedParentProfileResponse>(parentProfile);
            return response;
        }
    }
}