using AutoMapper;
using ExampleCode.Models;
using ExampleCode.Models.DTO;

namespace ExampleCode.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();

            CreateMap<UserModel, UserCreateDto>().ReverseMap();
            CreateMap<UserModel, UserUpdateDto>().ReverseMap();

        }
    }
}
