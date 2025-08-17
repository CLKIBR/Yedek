using Application.Features.FeatureFlags.Constants;
using Application.Features.FeatureFlags.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FeatureFlags.Constants.FeatureFlagsOperationClaims;

namespace Application.Features.FeatureFlags.Queries.GetById;

public class GetByIdFeatureFlagQuery : IRequest<GetByIdFeatureFlagResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdFeatureFlagQueryHandler : IRequestHandler<GetByIdFeatureFlagQuery, GetByIdFeatureFlagResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFeatureFlagRepository _featureFlagRepository;
        private readonly FeatureFlagBusinessRules _featureFlagBusinessRules;

        public GetByIdFeatureFlagQueryHandler(IMapper mapper, IFeatureFlagRepository featureFlagRepository, FeatureFlagBusinessRules featureFlagBusinessRules)
        {
            _mapper = mapper;
            _featureFlagRepository = featureFlagRepository;
            _featureFlagBusinessRules = featureFlagBusinessRules;
        }

        public async Task<GetByIdFeatureFlagResponse> Handle(GetByIdFeatureFlagQuery request, CancellationToken cancellationToken)
        {
            FeatureFlag? featureFlag = await _featureFlagRepository.GetAsync(predicate: ff => ff.Id == request.Id, cancellationToken: cancellationToken);
            await _featureFlagBusinessRules.FeatureFlagShouldExistWhenSelected(featureFlag);

            GetByIdFeatureFlagResponse response = _mapper.Map<GetByIdFeatureFlagResponse>(featureFlag);
            return response;
        }
    }
}