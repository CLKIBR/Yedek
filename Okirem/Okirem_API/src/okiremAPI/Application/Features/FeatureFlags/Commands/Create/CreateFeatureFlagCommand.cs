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

namespace Application.Features.FeatureFlags.Commands.Create;

public class CreateFeatureFlagCommand : IRequest<CreatedFeatureFlagResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool Enabled { get; set; }
    public string? Metadata { get; set; }

    public string[] Roles => [Admin, Write, FeatureFlagsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetFeatureFlags"];

    public class CreateFeatureFlagCommandHandler : IRequestHandler<CreateFeatureFlagCommand, CreatedFeatureFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeatureFlagRepository _featureFlagRepository;
        private readonly FeatureFlagBusinessRules _featureFlagBusinessRules;

        public CreateFeatureFlagCommandHandler(IMapper mapper, IFeatureFlagRepository featureFlagRepository,
                                         FeatureFlagBusinessRules featureFlagBusinessRules)
        {
            _mapper = mapper;
            _featureFlagRepository = featureFlagRepository;
            _featureFlagBusinessRules = featureFlagBusinessRules;
        }

        public async Task<CreatedFeatureFlagResponse> Handle(CreateFeatureFlagCommand request, CancellationToken cancellationToken)
        {
            FeatureFlag featureFlag = _mapper.Map<FeatureFlag>(request);

            await _featureFlagRepository.AddAsync(featureFlag);

            CreatedFeatureFlagResponse response = _mapper.Map<CreatedFeatureFlagResponse>(featureFlag);
            return response;
        }
    }
}