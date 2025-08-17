using Application.Features.TeacherProfiles.Commands.Create;
using Application.Features.TeacherProfiles.Commands.Delete;
using Application.Features.TeacherProfiles.Commands.Update;
using Application.Features.TeacherProfiles.Queries.GetById;
using Application.Features.TeacherProfiles.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.TeacherProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTeacherProfileCommand, TeacherProfile>();
        CreateMap<TeacherProfile, CreatedTeacherProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<UpdateTeacherProfileCommand, TeacherProfile>();
        CreateMap<TeacherProfile, UpdatedTeacherProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<DeleteTeacherProfileCommand, TeacherProfile>();
        CreateMap<TeacherProfile, DeletedTeacherProfileResponse>();

        CreateMap<TeacherProfile, GetByIdTeacherProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<TeacherProfile, GetListTeacherProfileListItemDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));
        CreateMap<IPaginate<TeacherProfile>, GetListResponse<GetListTeacherProfileListItemDto>>();
    }
}