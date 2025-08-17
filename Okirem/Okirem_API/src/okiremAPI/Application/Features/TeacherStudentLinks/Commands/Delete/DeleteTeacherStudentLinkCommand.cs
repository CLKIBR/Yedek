using Application.Features.TeacherStudentLinks.Constants;
using Application.Features.TeacherStudentLinks.Constants;
using Application.Features.TeacherStudentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.TeacherStudentLinks.Constants.TeacherStudentLinksOperationClaims;

namespace Application.Features.TeacherStudentLinks.Commands.Delete;

public class DeleteTeacherStudentLinkCommand : IRequest<DeletedTeacherStudentLinkResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, TeacherStudentLinksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTeacherStudentLinks"];

    public class DeleteTeacherStudentLinkCommandHandler : IRequestHandler<DeleteTeacherStudentLinkCommand, DeletedTeacherStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
        private readonly TeacherStudentLinkBusinessRules _teacherStudentLinkBusinessRules;

        public DeleteTeacherStudentLinkCommandHandler(IMapper mapper, ITeacherStudentLinkRepository teacherStudentLinkRepository,
                                         TeacherStudentLinkBusinessRules teacherStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherStudentLinkRepository = teacherStudentLinkRepository;
            _teacherStudentLinkBusinessRules = teacherStudentLinkBusinessRules;
        }

        public async Task<DeletedTeacherStudentLinkResponse> Handle(DeleteTeacherStudentLinkCommand request, CancellationToken cancellationToken)
        {
            TeacherStudentLink? teacherStudentLink = await _teacherStudentLinkRepository.GetAsync(predicate: tsl => tsl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherStudentLinkBusinessRules.TeacherStudentLinkShouldExistWhenSelected(teacherStudentLink);

            await _teacherStudentLinkRepository.DeleteAsync(teacherStudentLink!);

            DeletedTeacherStudentLinkResponse response = _mapper.Map<DeletedTeacherStudentLinkResponse>(teacherStudentLink);
            return response;
        }
    }
}