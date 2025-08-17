using Application.Features.TeacherStudentLinks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeacherStudentLinks.Constants.TeacherStudentLinksOperationClaims;

namespace Application.Features.TeacherStudentLinks.Queries.GetList;

public class GetListTeacherStudentLinkQuery : IRequest<GetListResponse<GetListTeacherStudentLinkListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTeacherStudentLinks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTeacherStudentLinks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTeacherStudentLinkQueryHandler : IRequestHandler<GetListTeacherStudentLinkQuery, GetListResponse<GetListTeacherStudentLinkListItemDto>>
    {
        private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
        private readonly IMapper _mapper;

        public GetListTeacherStudentLinkQueryHandler(ITeacherStudentLinkRepository teacherStudentLinkRepository, IMapper mapper)
        {
            _teacherStudentLinkRepository = teacherStudentLinkRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeacherStudentLinkListItemDto>> Handle(GetListTeacherStudentLinkQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeacherStudentLink> teacherStudentLinks = await _teacherStudentLinkRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeacherStudentLinkListItemDto> response = _mapper.Map<GetListResponse<GetListTeacherStudentLinkListItemDto>>(teacherStudentLinks);
            return response;
        }
    }
}