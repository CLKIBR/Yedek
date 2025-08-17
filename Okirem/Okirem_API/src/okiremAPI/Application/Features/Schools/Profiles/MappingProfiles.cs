using AutoMapper;
using Application.Features.Schools.Commands.Create;
using Application.Features.Schools.Commands.Delete;
using Application.Features.Schools.Commands.Update;
using Application.Features.Schools.Queries.GetById;
using Application.Features.Schools.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Schools.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateSchoolCommand, School>();
        CreateMap<School, CreatedSchoolResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

        CreateMap<UpdateSchoolCommand, School>();
        CreateMap<School, UpdatedSchoolResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

        CreateMap<DeleteSchoolCommand, School>();
        CreateMap<School, DeletedSchoolResponse>();

        CreateMap<School, GetByIdSchoolResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));

        CreateMap<School, GetListSchoolListItemDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (int)src.Type));
        CreateMap<IPaginate<School>, GetListResponse<GetListSchoolListItemDto>>();
    }
}