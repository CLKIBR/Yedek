using AutoMapper;
using Application.Features.TeacherParentLinks.Commands.Create;
using Application.Features.TeacherParentLinks.Commands.Delete;
using Application.Features.TeacherParentLinks.Commands.Update;
using Application.Features.TeacherParentLinks.Queries.GetById;
using Application.Features.TeacherParentLinks.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.TeacherParentLinks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTeacherParentLinkCommand, TeacherParentLink>();
        CreateMap<TeacherParentLink, CreatedTeacherParentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<UpdateTeacherParentLinkCommand, TeacherParentLink>();
        CreateMap<TeacherParentLink, UpdatedTeacherParentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<DeleteTeacherParentLinkCommand, TeacherParentLink>();
        CreateMap<TeacherParentLink, DeletedTeacherParentLinkResponse>();

        CreateMap<TeacherParentLink, GetByIdTeacherParentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<TeacherParentLink, GetListTeacherParentLinkListItemDto>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));
        CreateMap<IPaginate<TeacherParentLink>, GetListResponse<GetListTeacherParentLinkListItemDto>>();
    }
}