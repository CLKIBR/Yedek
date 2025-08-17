using Application.Features.ParentStudentLinks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ParentStudentLinks.Constants.ParentStudentLinksOperationClaims;

namespace Application.Features.ParentStudentLinks.Queries.GetList;

public class GetListParentStudentLinkQuery : IRequest<GetListResponse<GetListParentStudentLinkListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListParentStudentLinks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetParentStudentLinks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListParentStudentLinkQueryHandler : IRequestHandler<GetListParentStudentLinkQuery, GetListResponse<GetListParentStudentLinkListItemDto>>
    {
        private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
        private readonly IMapper _mapper;

        public GetListParentStudentLinkQueryHandler(IParentStudentLinkRepository parentStudentLinkRepository, IMapper mapper)
        {
            _parentStudentLinkRepository = parentStudentLinkRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListParentStudentLinkListItemDto>> Handle(GetListParentStudentLinkQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ParentStudentLink> parentStudentLinks = await _parentStudentLinkRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListParentStudentLinkListItemDto> response = _mapper.Map<GetListResponse<GetListParentStudentLinkListItemDto>>(parentStudentLinks);
            return response;
        }
    }
}