using AutoMapper;
using Application.Features.ParentProfiles.Commands.Create;
using Application.Features.ParentProfiles.Commands.Delete;
using Application.Features.ParentProfiles.Commands.Update;
using Application.Features.ParentProfiles.Queries.GetById;
using Application.Features.ParentProfiles.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ParentProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateParentProfileCommand, ParentProfile>();
        CreateMap<ParentProfile, CreatedParentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<UpdateParentProfileCommand, ParentProfile>();
        CreateMap<ParentProfile, UpdatedParentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<DeleteParentProfileCommand, ParentProfile>();
        CreateMap<ParentProfile, DeletedParentProfileResponse>();

        CreateMap<ParentProfile, GetByIdParentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<ParentProfile, GetListParentProfileListItemDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));
        CreateMap<IPaginate<ParentProfile>, GetListResponse<GetListParentProfileListItemDto>>();
    }
}