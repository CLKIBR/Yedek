using Application.Features.Schools.Constants;
using Application.Features.Schools.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Schools.Constants.SchoolsOperationClaims;

namespace Application.Features.Schools.Commands.Create;

public class CreateSchoolCommand : IRequest<CreatedSchoolResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }
    public required SchoolType Type { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Country { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Notes { get; set; }
    public Guid? TenantId { get; set; }

    public string[] Roles => [Admin, Write, SchoolsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSchools"];

    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, CreatedSchoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;
        private readonly SchoolBusinessRules _schoolBusinessRules;

        public CreateSchoolCommandHandler(IMapper mapper, ISchoolRepository schoolRepository,
                                         SchoolBusinessRules schoolBusinessRules)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _schoolBusinessRules = schoolBusinessRules;
        }

        public async Task<CreatedSchoolResponse> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            School school = _mapper.Map<School>(request);

            await _schoolRepository.AddAsync(school);

            CreatedSchoolResponse response = _mapper.Map<CreatedSchoolResponse>(school);
            return response;
        }
    }
}