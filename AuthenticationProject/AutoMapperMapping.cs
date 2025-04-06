using AuthenticationProject.DTOs;
using AuthenticationProject.Models.ResponseModels;
using AutoMapper;

namespace AuthenticationProject
{
    public class AutoMapperMapping : Profile
    {
        public AutoMapperMapping()
        {
            CreateMap<UserDTO, UserResponseModel>();
        }
    }
}
