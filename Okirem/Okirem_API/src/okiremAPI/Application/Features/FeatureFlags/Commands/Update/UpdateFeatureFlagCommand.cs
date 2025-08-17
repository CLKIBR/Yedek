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

namespace Application.Features.FeatureFlags.Commands.Update;

public class UpdateFeatureFlagCommand : IRequest<UpdatedFeatureFlagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool Enabled { get; set; }
    public string? Metadata { get; set; }

    public string[] Roles => [Admin, Write, FeatureFlagsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeatureFlags"];

    public class UpdateFeatureFlagCommandHandler : IRequestHandler<UpdateFeatureFlagCommand, UpdatedFeatureFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeatureFlagRepository _featureFlagRepository;
        private readonly FeatureFlagBusinessRules _featureFlagBusinessRules;

        public UpdateFeatureFlagCommandHandler(IMapper mapper, IFeatureFlagRepository featureFlagRepository,
                                         FeatureFlagBusinessRules featureFlagBusinessRules)
        {
            _mapper = mapper;
            _featureFlagRepository = featureFlagRepository;
            _featureFlagBusinessRules = featureFlagBusinessRules;
        }

        public async Task<UpdatedFeatureFlagResponse> Handle(UpdateFeatureFlagCommand request, CancellationToken cancellationToken)
        {
            FeatureFlag? featureFlag = await _featureFlagRepository.GetAsync(predicate: ff => ff.Id == request.Id, cancellationToken: cancellationToken);
            await _featureFlagBusinessRules.FeatureFlagShouldExistWhenSelected(featureFlag);
            featureFlag = _mapper.Map(request, featureFlag);

            await _featureFlagRepository.UpdateAsync(featureFlag!);

            UpdatedFeatureFlagResponse response = _mapper.Map<UpdatedFeatureFlagResponse>(featureFlag);
            return response;
        }
    }
}