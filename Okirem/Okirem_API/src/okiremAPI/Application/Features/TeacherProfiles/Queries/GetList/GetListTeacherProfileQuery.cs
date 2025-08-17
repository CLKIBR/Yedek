using Application.Features.TeacherProfiles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.TeacherProfiles.Constants.TeacherProfilesOperationClaims;

namespace Application.Features.TeacherProfiles.Queries.GetList;

public class GetListTeacherProfileQuery : IRequest<GetListResponse<GetListTeacherProfileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTeacherProfiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTeacherProfiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTeacherProfileQueryHandler : IRequestHandler<GetListTeacherProfileQuery, GetListResponse<GetListTeacherProfileListItemDto>>
    {
        private readonly ITeacherProfileRepository _teacherProfileRepository;
        private readonly IMapper _mapper;

        public GetListTeacherProfileQueryHandler(ITeacherProfileRepository teacherProfileRepository, IMapper mapper)
        {
            _teacherProfileRepository = teacherProfileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTeacherProfileListItemDto>> Handle(GetListTeacherProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TeacherProfile> teacherProfiles = await _teacherProfileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTeacherProfileListItemDto> response = _mapper.Map<GetListResponse<GetListTeacherProfileListItemDto>>(teacherProfiles);
            return response;
        }
    }
}