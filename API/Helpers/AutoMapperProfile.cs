using AutoMapper;
using Infrastructure.Dtos;
using Models.Extensions;

namespace API.Helpers;

public class AutoMapperProfile :Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(x => x.PhotoUrl,
                src => src.MapFrom(dest => dest.Photos
                    .FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age, 
                opt => 
                    opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();
        CreateMap<RegisterDTO, AppUser>();
    }
    
}