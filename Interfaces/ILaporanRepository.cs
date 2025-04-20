using POS.Api.DTOs;

namespace POS.Api.Interfaces
{
    public interface ILaporanRepository
    {
        Task<LaporanPenjualanDto> GetLaporanPenjualanAsync(DateTime from, DateTime to, string? userId = null);
        Task<IEnumerable<ProdukTerlarisDto>> GetProdukTerlarisAsync(DateTime from, DateTime to, int? top = 5);
    }
}
