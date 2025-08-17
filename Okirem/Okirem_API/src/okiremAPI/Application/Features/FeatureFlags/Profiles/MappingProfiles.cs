using Application.Features.FeatureFlags.Commands.Create;
using Application.Features.FeatureFlags.Commands.Delete;
using Application.Features.FeatureFlags.Commands.Update;
using Application.Features.FeatureFlags.Queries.GetById;
using Application.Features.FeatureFlags.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.FeatureFlags.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateFeatureFlagCommand, FeatureFlag>();
        CreateMap<FeatureFlag, CreatedFeatureFlagResponse>();

        CreateMap<UpdateFeatureFlagCommand, FeatureFlag>();
        CreateMap<FeatureFlag, UpdatedFeatureFlagResponse>();

        CreateMap<DeleteFeatureFlagCommand, FeatureFlag>();
        CreateMap<FeatureFlag, DeletedFeatureFlagResponse>();

        CreateMap<FeatureFlag, GetByIdFeatureFlagResponse>();

        CreateMap<FeatureFlag, GetListFeatureFlagListItemDto>();
        CreateMap<IPaginate<FeatureFlag>, GetListResponse<GetListFeatureFlagListItemDto>>();
    }
}