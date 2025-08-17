using Application.Features.StudentProfiles.Constants;
using Application.Features.StudentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentProfiles.Constants.StudentProfilesOperationClaims;

namespace Application.Features.StudentProfiles.Queries.GetById;

public class GetByIdStudentProfileQuery : IRequest<GetByIdStudentProfileResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdStudentProfileQueryHandler : IRequestHandler<GetByIdStudentProfileQuery, GetByIdStudentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentProfileRepository _studentProfileRepository;
        private readonly StudentProfileBusinessRules _studentProfileBusinessRules;

        public GetByIdStudentProfileQueryHandler(IMapper mapper, IStudentProfileRepository studentProfileRepository, StudentProfileBusinessRules studentProfileBusinessRules)
        {
            _mapper = mapper;
            _studentProfileRepository = studentProfileRepository;
            _studentProfileBusinessRules = studentProfileBusinessRules;
        }

        public async Task<GetByIdStudentProfileResponse> Handle(GetByIdStudentProfileQuery request, CancellationToken cancellationToken)
        {
            StudentProfile? studentProfile = await _studentProfileRepository.GetAsync(predicate: sp => sp.Id == request.Id, cancellationToken: cancellationToken);
            await _studentProfileBusinessRules.StudentProfileShouldExistWhenSelected(studentProfile);

            GetByIdStudentProfileResponse response = _mapper.Map<GetByIdStudentProfileResponse>(studentProfile);
            return response;
        }
    }
}