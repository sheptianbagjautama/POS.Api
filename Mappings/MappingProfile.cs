using AutoMapper;
using POS.Api.DTOs;
using POS.Api.Entities;
using POS.Api.Models;

namespace POS.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); //akan isi manual karena ambil dari repository
            CreateMap<Kategori, KategoriDto>().ReverseMap(); //ReverseMap() untuk dua arah
            CreateMap<CreateKategoriDto, Kategori>();
            CreateMap<Produk, ProdukDto>().ReverseMap();
            CreateMap<CreateProdukDto, Produk>();
            CreateMap<Meja, MejaDto>().ReverseMap();
            CreateMap<CreateMejaDto, Meja>();
        }
    }
}
