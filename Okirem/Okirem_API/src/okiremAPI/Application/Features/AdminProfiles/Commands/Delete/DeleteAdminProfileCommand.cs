using Application.Features.AdminProfiles.Constants;
using Application.Features.AdminProfiles.Constants;
using Application.Features.AdminProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AdminProfiles.Constants.AdminProfilesOperationClaims;

namespace Application.Features.AdminProfiles.Commands.Delete;

public class DeleteAdminProfileCommand : IRequest<DeletedAdminProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, AdminProfilesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAdminProfiles"];

    public class DeleteAdminProfileCommandHandler : IRequestHandler<DeleteAdminProfileCommand, DeletedAdminProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly AdminProfileBusinessRules _adminProfileBusinessRules;

        public DeleteAdminProfileCommandHandler(IMapper mapper, IAdminProfileRepository adminProfileRepository,
                                         AdminProfileBusinessRules adminProfileBusinessRules)
        {
            _mapper = mapper;
            _adminProfileRepository = adminProfileRepository;
            _adminProfileBusinessRules = adminProfileBusinessRules;
        }

        public async Task<DeletedAdminProfileResponse> Handle(DeleteAdminProfileCommand request, CancellationToken cancellationToken)
        {
            AdminProfile? adminProfile = await _adminProfileRepository.GetAsync(predicate: ap => ap.Id == request.Id, cancellationToken: cancellationToken);
            await _adminProfileBusinessRules.AdminProfileShouldExistWhenSelected(adminProfile);

            await _adminProfileRepository.DeleteAsync(adminProfile!);

            DeletedAdminProfileResponse response = _mapper.Map<DeletedAdminProfileResponse>(adminProfile);
            return response;
        }
    }
}