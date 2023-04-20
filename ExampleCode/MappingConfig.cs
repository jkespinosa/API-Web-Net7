using AutoMapper;
using ExampleCode.Models;
using ExampleCode.Models.DTO;

namespace ExampleCode.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

        }
    }
}
