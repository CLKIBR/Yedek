using Application.Features.TeacherStudentLinks.Constants;
using Application.Features.TeacherStudentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeacherStudentLinks.Constants.TeacherStudentLinksOperationClaims;

namespace Application.Features.TeacherStudentLinks.Queries.GetById;

public class GetByIdTeacherStudentLinkQuery : IRequest<GetByIdTeacherStudentLinkResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdTeacherStudentLinkQueryHandler : IRequestHandler<GetByIdTeacherStudentLinkQuery, GetByIdTeacherStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherStudentLinkRepository _teacherStudentLinkRepository;
        private readonly TeacherStudentLinkBusinessRules _teacherStudentLinkBusinessRules;

        public GetByIdTeacherStudentLinkQueryHandler(IMapper mapper, ITeacherStudentLinkRepository teacherStudentLinkRepository, TeacherStudentLinkBusinessRules teacherStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherStudentLinkRepository = teacherStudentLinkRepository;
            _teacherStudentLinkBusinessRules = teacherStudentLinkBusinessRules;
        }

        public async Task<GetByIdTeacherStudentLinkResponse> Handle(GetByIdTeacherStudentLinkQuery request, CancellationToken cancellationToken)
        {
            TeacherStudentLink? teacherStudentLink = await _teacherStudentLinkRepository.GetAsync(predicate: tsl => tsl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherStudentLinkBusinessRules.TeacherStudentLinkShouldExistWhenSelected(teacherStudentLink);

            GetByIdTeacherStudentLinkResponse response = _mapper.Map<GetByIdTeacherStudentLinkResponse>(teacherStudentLink);
            return response;
        }
    }
}