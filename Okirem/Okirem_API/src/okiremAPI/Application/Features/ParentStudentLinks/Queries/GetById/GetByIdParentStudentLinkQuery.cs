using Application.Features.ParentStudentLinks.Constants;
using Application.Features.ParentStudentLinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ParentStudentLinks.Constants.ParentStudentLinksOperationClaims;

namespace Application.Features.ParentStudentLinks.Queries.GetById;

public class GetByIdParentStudentLinkQuery : IRequest<GetByIdParentStudentLinkResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdParentStudentLinkQueryHandler : IRequestHandler<GetByIdParentStudentLinkQuery, GetByIdParentStudentLinkResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParentStudentLinkRepository _parentStudentLinkRepository;
        private readonly ParentStudentLinkBusinessRules _parentStudentLinkBusinessRules;

        public GetByIdParentStudentLinkQueryHandler(IMapper mapper, IParentStudentLinkRepository parentStudentLinkRepository, ParentStudentLinkBusinessRules parentStudentLinkBusinessRules)
        {
            _mapper = mapper;
            _parentStudentLinkRepository = parentStudentLinkRepository;
            _parentStudentLinkBusinessRules = parentStudentLinkBusinessRules;
        }

        public async Task<GetByIdParentStudentLinkResponse> Handle(GetByIdParentStudentLinkQuery request, CancellationToken cancellationToken)
        {
            ParentStudentLink? parentStudentLink = await _parentStudentLinkRepository.GetAsync(predicate: psl => psl.Id == request.Id, cancellationToken: cancellationToken);
            await _parentStudentLinkBusinessRules.ParentStudentLinkShouldExistWhenSelected(parentStudentLink);

            GetByIdParentStudentLinkResponse response = _mapper.Map<GetByIdParentStudentLinkResponse>(parentStudentLink);
            return response;
        }
    }
}