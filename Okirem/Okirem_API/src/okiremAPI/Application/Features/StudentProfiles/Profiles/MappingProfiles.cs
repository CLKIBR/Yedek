using AutoMapper;
using Application.Features.StudentProfiles.Commands.Create;
using Application.Features.StudentProfiles.Commands.Delete;
using Application.Features.StudentProfiles.Commands.Update;
using Application.Features.StudentProfiles.Queries.GetById;
using Application.Features.StudentProfiles.Queries.GetList;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.StudentProfiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateStudentProfileCommand, StudentProfile>();
        CreateMap<StudentProfile, CreatedStudentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<UpdateStudentProfileCommand, StudentProfile>();
        CreateMap<StudentProfile, UpdatedStudentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<DeleteStudentProfileCommand, StudentProfile>();
        CreateMap<StudentProfile, DeletedStudentProfileResponse>();

        CreateMap<StudentProfile, GetByIdStudentProfileResponse>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));

        CreateMap<StudentProfile, GetListStudentProfileListItemDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender));
        CreateMap<IPaginate<StudentProfile>, GetListResponse<GetListStudentProfileListItemDto>>();
    }
}