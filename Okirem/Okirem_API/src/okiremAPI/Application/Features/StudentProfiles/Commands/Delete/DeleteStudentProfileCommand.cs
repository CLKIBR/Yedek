using Application.Features.StudentProfiles.Constants;
using Application.Features.StudentProfiles.Constants;
using Application.Features.StudentProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentProfiles.Constants.StudentProfilesOperationClaims;

namespace Application.Features.StudentProfiles.Commands.Delete;

public class DeleteStudentProfileCommand : IRequest<DeletedStudentProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, StudentProfilesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStudentProfiles"];

    public class DeleteStudentProfileCommandHandler : IRequestHandler<DeleteStudentProfileCommand, DeletedStudentProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentProfileRepository _studentProfileRepository;
        private readonly StudentProfileBusinessRules _studentProfileBusinessRules;

        public DeleteStudentProfileCommandHandler(IMapper mapper, IStudentProfileRepository studentProfileRepository,
                                         StudentProfileBusinessRules studentProfileBusinessRules)
        {
            _mapper = mapper;
            _studentProfileRepository = studentProfileRepository;
            _studentProfileBusinessRules = studentProfileBusinessRules;
        }

        public async Task<DeletedStudentProfileResponse> Handle(DeleteStudentProfileCommand request, CancellationToken cancellationToken)
        {
            StudentProfile? studentProfile = await _studentProfileRepository.GetAsync(predicate: sp => sp.Id == request.Id, cancellationToken: cancellationToken);
            await _studentProfileBusinessRules.StudentProfileShouldExistWhenSelected(studentProfile);

            await _studentProfileRepository.DeleteAsync(studentProfile!);

            DeletedStudentProfileResponse response = _mapper.Map<DeletedStudentProfileResponse>(studentProfile);
            return response;
        }
    }
}