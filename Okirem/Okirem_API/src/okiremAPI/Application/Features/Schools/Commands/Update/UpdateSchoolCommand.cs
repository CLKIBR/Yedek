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

namespace Application.Features.Schools.Commands.Update;

public class UpdateSchoolCommand : IRequest<UpdatedSchoolResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
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

    public string[] Roles => [Admin, Write, SchoolsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSchools"];

    public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, UpdatedSchoolResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;
        private readonly SchoolBusinessRules _schoolBusinessRules;

        public UpdateSchoolCommandHandler(IMapper mapper, ISchoolRepository schoolRepository,
                                         SchoolBusinessRules schoolBusinessRules)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
            _schoolBusinessRules = schoolBusinessRules;
        }

        public async Task<UpdatedSchoolResponse> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
        {
            School? school = await _schoolRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _schoolBusinessRules.SchoolShouldExistWhenSelected(school);
            school = _mapper.Map(request, school);

            await _schoolRepository.UpdateAsync(school!);

            UpdatedSchoolResponse response = _mapper.Map<UpdatedSchoolResponse>(school);
            return response;
        }
    }
}