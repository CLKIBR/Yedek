using Application.Features.FeatureFlags.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.FeatureFlags.Constants.FeatureFlagsOperationClaims;

namespace Application.Features.FeatureFlags.Queries.GetList;

public class GetListFeatureFlagQuery : IRequest<GetListResponse<GetListFeatureFlagListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListFeatureFlags({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetFeatureFlags";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFeatureFlagQueryHandler : IRequestHandler<GetListFeatureFlagQuery, GetListResponse<GetListFeatureFlagListItemDto>>
    {
        private readonly IFeatureFlagRepository _featureFlagRepository;
        private readonly IMapper _mapper;

        public GetListFeatureFlagQueryHandler(IFeatureFlagRepository featureFlagRepository, IMapper mapper)
        {
            _featureFlagRepository = featureFlagRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFeatureFlagListItemDto>> Handle(GetListFeatureFlagQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FeatureFlag> featureFlags = await _featureFlagRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFeatureFlagListItemDto> response = _mapper.Map<GetListResponse<GetListFeatureFlagListItemDto>>(featureFlags);
            return response;
        }
    }
}