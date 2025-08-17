using Application.Features.TeacherParentLinks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeacherParentLinks.Constants.TeacherParentLinksOperationClaims;

namespace Application.Features.TeacherParentLinks.Queries.GetList;

public class GetListTeacherParentLinkQuery : IRequest<GetListResponse<GetListTeacherParentLinkListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTeacherParentLinks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTeacherParentLinks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTeacherParentLinkQueryHandler : IRequestHandler<GetListTeacherParentLinkQuery, GetListResponse<GetListTeacherParentLinkListItemDto>>
    {
        private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
        private readonly IMapper _mapper;

        public GetListTeacherParentLinkQueryHandler(ITeacherParentLinkRepository teacherParentLinkRepository, IMapper mapper)
        {
            _teacherParentLinkRepository = teacherParentLinkRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeacherParentLinkListItemDto>> Handle(GetListTeacherParentLinkQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeacherParentLink> teacherParentLinks = await _teacherParentLinkRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeacherParentLinkListItemDto> response = _mapper.Map<GetListResponse<GetListTeacherParentLinkListItemDto>>(teacherParentLinks);
            return response;
        }
    }
}