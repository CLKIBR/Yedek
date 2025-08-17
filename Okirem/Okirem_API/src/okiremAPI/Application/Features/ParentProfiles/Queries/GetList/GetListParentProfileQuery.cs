using Application.Features.ParentProfiles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ParentProfiles.Constants.ParentProfilesOperationClaims;

namespace Application.Features.ParentProfiles.Queries.GetList;

public class GetListParentProfileQuery : IRequest<GetListResponse<GetListParentProfileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListParentProfiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetParentProfiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListParentProfileQueryHandler : IRequestHandler<GetListParentProfileQuery, GetListResponse<GetListParentProfileListItemDto>>
    {
        private readonly IParentProfileRepository _parentProfileRepository;
        private readonly IMapper _mapper;

        public GetListParentProfileQueryHandler(IParentProfileRepository parentProfileRepository, IMapper mapper)
        {
            _parentProfileRepository = parentProfileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListParentProfileListItemDto>> Handle(GetListParentProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ParentProfile> parentProfiles = await _parentProfileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListParentProfileListItemDto> response = _mapper.Map<GetListResponse<GetListParentProfileListItemDto>>(parentProfiles);
            return response;
        }
    }
}