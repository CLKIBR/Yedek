using AutoMapper;
using Application.Features.TeacherStudentLinks.Commands.Create;
using Application.Features.TeacherStudentLinks.Commands.Delete;
using Application.Features.TeacherStudentLinks.Commands.Update;
using Application.Features.TeacherStudentLinks.Queries.GetById;
using Application.Features.TeacherStudentLinks.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.TeacherStudentLinks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTeacherStudentLinkCommand, TeacherStudentLink>();
        CreateMap<TeacherStudentLink, CreatedTeacherStudentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<UpdateTeacherStudentLinkCommand, TeacherStudentLink>();
        CreateMap<TeacherStudentLink, UpdatedTeacherStudentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<DeleteTeacherStudentLinkCommand, TeacherStudentLink>();
        CreateMap<TeacherStudentLink, DeletedTeacherStudentLinkResponse>();

        CreateMap<TeacherStudentLink, GetByIdTeacherStudentLinkResponse>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));

        CreateMap<TeacherStudentLink, GetListTeacherStudentLinkListItemDto>()
            .ForMember(dest => dest.LinkRole, opt => opt.MapFrom(src => (int)src.LinkRole));
        CreateMap<IPaginate<TeacherStudentLink>, GetListResponse<GetListTeacherStudentLinkListItemDto>>();
    }
}