using AutoMapper;
using Application.Features.AdminProfiles.Commands.Create;
using Application.Features.AdminProfiles.Commands.Delete;
using Application.Features.AdminProfiles.Commands.Update;
using Application.Features.AdminProfiles.Queries.GetById;
using Application.Features.AdminProfiles.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.AdminProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAdminProfileCommand, AdminProfile>();
        CreateMap<AdminProfile, CreatedAdminProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<UpdateAdminProfileCommand, AdminProfile>();
        CreateMap<AdminProfile, UpdatedAdminProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<DeleteAdminProfileCommand, AdminProfile>();
        CreateMap<AdminProfile, DeletedAdminProfileResponse>();

        CreateMap<AdminProfile, GetByIdAdminProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<AdminProfile, GetListAdminProfileListItemDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));
        CreateMap<IPaginate<AdminProfile>, GetListResponse<GetListAdminProfileListItemDto>>();
    }
}