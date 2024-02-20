using AutoMapper;
using usue_online_tests.Dto;
using usue_online_tests.Models;

namespace usue_online_tests.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(dto => dto.Role,
                expression => expression.MapFrom(user => user.Role.ToString()));
    }
}