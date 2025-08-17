using Application.Features.StudentProfiles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentProfiles.Constants.StudentProfilesOperationClaims;

namespace Application.Features.StudentProfiles.Queries.GetList;

public class GetListStudentProfileQuery : IRequest<GetListResponse<GetListStudentProfileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListStudentProfiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetStudentProfiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentProfileQueryHandler : IRequestHandler<GetListStudentProfileQuery, GetListResponse<GetListStudentProfileListItemDto>>
    {
        private readonly IStudentProfileRepository _studentProfileRepository;
        private readonly IMapper _mapper;

        public GetListStudentProfileQueryHandler(IStudentProfileRepository studentProfileRepository, IMapper mapper)
        {
            _studentProfileRepository = studentProfileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentProfileListItemDto>> Handle(GetListStudentProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentProfile> studentProfiles = await _studentProfileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentProfileListItemDto> response = _mapper.Map<GetListResponse<GetListStudentProfileListItemDto>>(studentProfiles);
            return response;
        }
    }
}