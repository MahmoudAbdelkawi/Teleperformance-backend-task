using Authentication_CRUD_Operation.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Authentication_CRUD_Operation.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserDto, IdentityUser>().ReverseMap();
        }
    }
}
