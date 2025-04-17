using AutoMapper;
using POS.Api.DTOs;
using POS.Api.Models;

namespace POS.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); //akan isi manual karena ambil dari repository

        }
    }
}
