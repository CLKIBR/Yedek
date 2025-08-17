using Application.Features.Classrooms.Constants;
using Application.Features.Classrooms.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Classrooms.Constants.ClassroomsOperationClaims;

namespace Application.Features.Classrooms.Commands.Create;

public class CreateClassroomCommand : IRequest<CreatedClassroomResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required Guid SchoolId { get; set; }
    public string? Grade { get; set; }
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, ClassroomsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetClassrooms"];

    public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, CreatedClassroomResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassroomRepository _classroomRepository;
        private readonly ClassroomBusinessRules _classroomBusinessRules;

        public CreateClassroomCommandHandler(IMapper mapper, IClassroomRepository classroomRepository,
                                         ClassroomBusinessRules classroomBusinessRules)
        {
            _mapper = mapper;
            _classroomRepository = classroomRepository;
            _classroomBusinessRules = classroomBusinessRules;
        }

        public async Task<CreatedClassroomResponse> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
        {
            Classroom classroom = _mapper.Map<Classroom>(request);

            await _classroomRepository.AddAsync(classroom);

            CreatedClassroomResponse response = _mapper.Map<CreatedClassroomResponse>(classroom);
            return response;
        }
    }
}