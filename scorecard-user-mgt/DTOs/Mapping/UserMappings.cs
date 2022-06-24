using AutoMapper;
using scorecard_user_mgt.Models;

namespace scorecard_user_mgt.DTOs.Mapping
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<RegistrationDto, User>().ReverseMap();
            CreateMap<User, GetAllUserResponseDto>().ReverseMap();
            CreateMap<UserDetailResponseDTO, User>().ReverseMap();
        }  
    }
}