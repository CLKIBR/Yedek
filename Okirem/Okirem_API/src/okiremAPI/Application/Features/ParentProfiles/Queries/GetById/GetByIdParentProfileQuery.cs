using Application.Features.ParentProfiles.Constants;
using Application.Features.ParentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ParentProfiles.Constants.ParentProfilesOperationClaims;

namespace Application.Features.ParentProfiles.Queries.GetById;

public class GetByIdParentProfileQuery : IRequest<GetByIdParentProfileResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdParentProfileQueryHandler : IRequestHandler<GetByIdParentProfileQuery, GetByIdParentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentProfileRepository _parentProfileRepository;
        private readonly ParentProfileBusinessRules _parentProfileBusinessRules;

        public GetByIdParentProfileQueryHandler(IMapper mapper, IParentProfileRepository parentProfileRepository, ParentProfileBusinessRules parentProfileBusinessRules)
        {
            _mapper = mapper;
            _parentProfileRepository = parentProfileRepository;
            _parentProfileBusinessRules = parentProfileBusinessRules;
        }

        public async Task<GetByIdParentProfileResponse> Handle(GetByIdParentProfileQuery request, CancellationToken cancellationToken)
        {
            ParentProfile? parentProfile = await _parentProfileRepository.GetAsync(predicate: pp => pp.Id == request.Id, cancellationToken: cancellationToken);
            await _parentProfileBusinessRules.ParentProfileShouldExistWhenSelected(parentProfile);

            GetByIdParentProfileResponse response = _mapper.Map<GetByIdParentProfileResponse>(parentProfile);
            return response;
        }
    }
}