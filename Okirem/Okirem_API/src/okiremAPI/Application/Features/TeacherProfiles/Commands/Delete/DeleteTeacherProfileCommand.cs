using Application.Features.TeacherProfiles.Constants;
using Application.Features.TeacherProfiles.Constants;
using Application.Features.TeacherProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.TeacherProfiles.Constants.TeacherProfilesOperationClaims;

namespace Application.Features.TeacherProfiles.Commands.Delete;

public class DeleteTeacherProfileCommand : IRequest<DeletedTeacherProfileResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, TeacherProfilesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherProfiles"];

    public class DeleteTeacherProfileCommandHandler : IRequestHandler<DeleteTeacherProfileCommand, DeletedTeacherProfileResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherProfileRepository _teacherProfileRepository;
        private readonly TeacherProfileBusinessRules _teacherProfileBusinessRules;

        public DeleteTeacherProfileCommandHandler(IMapper mapper, ITeacherProfileRepository teacherProfileRepository,
                                         TeacherProfileBusinessRules teacherProfileBusinessRules)
        {
            _mapper = mapper;
            _teacherProfileRepository = teacherProfileRepository;
            _teacherProfileBusinessRules = teacherProfileBusinessRules;
        }

        public async Task<DeletedTeacherProfileResponse> Handle(DeleteTeacherProfileCommand request, CancellationToken cancellationToken)
        {
            TeacherProfile? teacherProfile = await _teacherProfileRepository.GetAsync(predicate: tp => tp.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherProfileBusinessRules.TeacherProfileShouldExistWhenSelected(teacherProfile);

            await _teacherProfileRepository.DeleteAsync(teacherProfile!);

            DeletedTeacherProfileResponse response = _mapper.Map<DeletedTeacherProfileResponse>(teacherProfile);
            return response;
        }
    }
}