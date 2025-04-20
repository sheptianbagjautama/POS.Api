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
            CreateMap<Pesanan, PesananDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src =>
                src.ProdukPesanan.Select(pp => new ItemPesananReadDto
                {
                    ProdukId = pp.ProdukId,
                    NamaProduk = pp.Produk != null ? pp.Produk.Nama : "(produk tidak ditemukan)" ,
                    HargaProduk = pp.Produk != null? pp.Produk.Harga : 0m,
                    Jumlah = pp.Jumlah
                })));

            CreateMap<CreatePesananDto, Pesanan>()
                .ForMember(dest => dest.ProdukPesanan, opt => opt.MapFrom(src =>
                    src.Items.Select(i => new ProdukPesanan
                    {
                        ProdukId = i.ProdukId,
                        Jumlah = i.Jumlah
                    })));
        }
    }
}
