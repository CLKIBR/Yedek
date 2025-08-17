using Application.Features.FeatureFlags.Constants;
using Application.Features.FeatureFlags.Constants;
using Application.Features.FeatureFlags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.FeatureFlags.Constants.FeatureFlagsOperationClaims;

namespace Application.Features.FeatureFlags.Commands.Delete;

public class DeleteFeatureFlagCommand : IRequest<DeletedFeatureFlagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, FeatureFlagsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeatureFlags"];

    public class DeleteFeatureFlagCommandHandler : IRequestHandler<DeleteFeatureFlagCommand, DeletedFeatureFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeatureFlagRepository _featureFlagRepository;
        private readonly FeatureFlagBusinessRules _featureFlagBusinessRules;

        public DeleteFeatureFlagCommandHandler(IMapper mapper, IFeatureFlagRepository featureFlagRepository,
                                         FeatureFlagBusinessRules featureFlagBusinessRules)
        {
            _mapper = mapper;
            _featureFlagRepository = featureFlagRepository;
            _featureFlagBusinessRules = featureFlagBusinessRules;
        }

        public async Task<DeletedFeatureFlagResponse> Handle(DeleteFeatureFlagCommand request, CancellationToken cancellationToken)
        {
            FeatureFlag? featureFlag = await _featureFlagRepository.GetAsync(predicate: ff => ff.Id == request.Id, cancellationToken: cancellationToken);
            await _featureFlagBusinessRules.FeatureFlagShouldExistWhenSelected(featureFlag);

            await _featureFlagRepository.DeleteAsync(featureFlag!);

            DeletedFeatureFlagResponse response = _mapper.Map<DeletedFeatureFlagResponse>(featureFlag);
            return response;
        }
    }
}