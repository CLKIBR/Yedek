using Application.Features.TeacherProfiles.Constants;
using Application.Features.TeacherProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeacherProfiles.Constants.TeacherProfilesOperationClaims;

namespace Application.Features.TeacherProfiles.Queries.GetById;

public class GetByIdTeacherProfileQuery : IRequest<GetByIdTeacherProfileResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdTeacherProfileQueryHandler : IRequestHandler<GetByIdTeacherProfileQuery, GetByIdTeacherProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherProfileRepository _teacherProfileRepository;
        private readonly TeacherProfileBusinessRules _teacherProfileBusinessRules;

        public GetByIdTeacherProfileQueryHandler(IMapper mapper, ITeacherProfileRepository teacherProfileRepository, TeacherProfileBusinessRules teacherProfileBusinessRules)
        {
            _mapper = mapper;
            _teacherProfileRepository = teacherProfileRepository;
            _teacherProfileBusinessRules = teacherProfileBusinessRules;
        }

        public async Task<GetByIdTeacherProfileResponse> Handle(GetByIdTeacherProfileQuery request, CancellationToken cancellationToken)
        {
            TeacherProfile? teacherProfile = await _teacherProfileRepository.GetAsync(predicate: tp => tp.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherProfileBusinessRules.TeacherProfileShouldExistWhenSelected(teacherProfile);

            GetByIdTeacherProfileResponse response = _mapper.Map<GetByIdTeacherProfileResponse>(teacherProfile);
            return response;
        }
    }
}