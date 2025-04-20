using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.DTOs;
using POS.Api.Interfaces;

namespace POS.Api.Repositories
{
    public class LaporanRepository : ILaporanRepository
    {
        private readonly ApplicationDbContext context;

        public LaporanRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<LaporanPenjualanDto> GetLaporanPenjualanAsync(DateTime from, DateTime to, string? userId = null)
        {
            var endOfDay = to.Date.AddDays(1).AddTicks(-1);

            var query = context.Pesanan
                .Include(p => p.ProdukPesanan)
                .Include(p => p.User)
                .Where(p => p.Status == "Paid" && p.CreatedAt >= from && p.CreatedAt <= endOfDay);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(p => p.UserId == userId);

            var pesanan = await query.ToListAsync();

            var totalPendapatan = pesanan.Sum(p => p.TotalHarga);
            var totalPesanan = pesanan.Count;
            var totalProduk = pesanan.SelectMany(p => p.ProdukPesanan).Sum(pp => pp.Jumlah);
            var namaKasir = await context.Users.FindAsync(userId);

            return new LaporanPenjualanDto
            {
                DariTanggal = from,
                SampaiTanggal = to,
                TotalPesanan = totalPesanan,
                TotalPendapatan = totalPendapatan,
                TotalProdukTerjual = totalProduk,
                NamaKasir = namaKasir != null ? namaKasir.FullName : ""
            };
        }

        public async Task<IEnumerable<ProdukTerlarisDto>> GetProdukTerlarisAsync(DateTime from, DateTime to, int? top = 5)
        {
            var endOfDay = to.Date.AddDays(1).AddTicks(-1);

            var data = await context.ProdukPesanan
                .Include(pp => pp.Pesanan)
                .Include(pp => pp.Produk)
                .Where(pp => pp.Pesanan!.Status == "Paid" &&
                             pp.Pesanan.CreatedAt >= from &&
                             pp.Pesanan.CreatedAt <= endOfDay)
                .GroupBy(pp => new {pp.ProdukId, pp.Produk!.Nama})
                .Select(g => new ProdukTerlarisDto
                {
                    ProdukId = g.Key.ProdukId,
                    NamaProduk = g.Key.Nama,
                    TotalTerjual = g.Sum(pp => pp.Jumlah)
                })
                .OrderByDescending(p => p.TotalTerjual)
                .Take(top ?? 5)
                .ToListAsync();

            return data;
        }
    }
}
