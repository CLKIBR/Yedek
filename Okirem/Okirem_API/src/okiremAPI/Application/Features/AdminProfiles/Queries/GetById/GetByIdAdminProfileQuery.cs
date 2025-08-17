using Application.Features.AdminProfiles.Constants;
using Application.Features.AdminProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AdminProfiles.Constants.AdminProfilesOperationClaims;

namespace Application.Features.AdminProfiles.Queries.GetById;

public class GetByIdAdminProfileQuery : IRequest<GetByIdAdminProfileResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAdminProfileQueryHandler : IRequestHandler<GetByIdAdminProfileQuery, GetByIdAdminProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly AdminProfileBusinessRules _adminProfileBusinessRules;

        public GetByIdAdminProfileQueryHandler(IMapper mapper, IAdminProfileRepository adminProfileRepository, AdminProfileBusinessRules adminProfileBusinessRules)
        {
            _mapper = mapper;
            _adminProfileRepository = adminProfileRepository;
            _adminProfileBusinessRules = adminProfileBusinessRules;
        }

        public async Task<GetByIdAdminProfileResponse> Handle(GetByIdAdminProfileQuery request, CancellationToken cancellationToken)
        {
            AdminProfile? adminProfile = await _adminProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _adminProfileBusinessRules.AdminProfileShouldExistWhenSelected(adminProfile);

            GetByIdAdminProfileResponse response = _mapper.Map<GetByIdAdminProfileResponse>(adminProfile);
            return response;
        }
    }
}