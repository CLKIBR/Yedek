using AutoMapper;
using Application.Features.ParentStudentLinks.Commands.Create;
using Application.Features.ParentStudentLinks.Commands.Delete;
using Application.Features.ParentStudentLinks.Commands.Update;
using Application.Features.ParentStudentLinks.Queries.GetById;
using Application.Features.ParentStudentLinks.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.ParentStudentLinks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateParentStudentLinkCommand, ParentStudentLink>();
        CreateMap<ParentStudentLink, CreatedParentStudentLinkResponse>()
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => (int)src.Relationship));

        CreateMap<UpdateParentStudentLinkCommand, ParentStudentLink>();
        CreateMap<ParentStudentLink, UpdatedParentStudentLinkResponse>()
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => (int)src.Relationship));

        CreateMap<DeleteParentStudentLinkCommand, ParentStudentLink>();
        CreateMap<ParentStudentLink, DeletedParentStudentLinkResponse>();

        CreateMap<ParentStudentLink, GetByIdParentStudentLinkResponse>()
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => (int)src.Relationship));

        CreateMap<ParentStudentLink, GetListParentStudentLinkListItemDto>()
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => (int)src.Relationship));
        CreateMap<IPaginate<ParentStudentLink>, GetListResponse<GetListParentStudentLinkListItemDto>>();
    }
}