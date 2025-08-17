using Application.Features.TeacherParentLinks.Constants;
using Application.Features.TeacherParentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TeacherParentLinks.Constants.TeacherParentLinksOperationClaims;

namespace Application.Features.TeacherParentLinks.Queries.GetById;

public class GetByIdTeacherParentLinkQuery : IRequest<GetByIdTeacherParentLinkResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdTeacherParentLinkQueryHandler : IRequestHandler<GetByIdTeacherParentLinkQuery, GetByIdTeacherParentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherParentLinkRepository _teacherParentLinkRepository;
        private readonly TeacherParentLinkBusinessRules _teacherParentLinkBusinessRules;

        public GetByIdTeacherParentLinkQueryHandler(IMapper mapper, ITeacherParentLinkRepository teacherParentLinkRepository, TeacherParentLinkBusinessRules teacherParentLinkBusinessRules)
        {
            _mapper = mapper;
            _teacherParentLinkRepository = teacherParentLinkRepository;
            _teacherParentLinkBusinessRules = teacherParentLinkBusinessRules;
        }

        public async Task<GetByIdTeacherParentLinkResponse> Handle(GetByIdTeacherParentLinkQuery request, CancellationToken cancellationToken)
        {
            TeacherParentLink? teacherParentLink = await _teacherParentLinkRepository.GetAsync(predicate: tpl => tpl.Id == request.Id, cancellationToken: cancellationToken);
            await _teacherParentLinkBusinessRules.TeacherParentLinkShouldExistWhenSelected(teacherParentLink);

            GetByIdTeacherParentLinkResponse response = _mapper.Map<GetByIdTeacherParentLinkResponse>(teacherParentLink);
            return response;
        }
    }
}