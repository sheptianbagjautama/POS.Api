using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Repositories
{
    public class PesananRepository : IPesananRepository
    {
        private readonly ApplicationDbContext context;

        public PesananRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Pesanan>> GetAllAsync()
        {
            return await context.Pesanan
                .Include(p => p.ProdukPesanan)
                .ThenInclude(pp => pp.Produk)
                .ToListAsync();
        }

        public async Task<Pesanan?> GetByIdAsync(int id)
        {
            return await context.Pesanan
                .Include(p => p.ProdukPesanan)
                .ThenInclude(pp => pp.Produk)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pesanan> CreateAsync(Pesanan pesanan)
        {
            context.Pesanan.Add(pesanan);
            await context.SaveChangesAsync();

            var result = await context.Pesanan
                .Include(p => p.ProdukPesanan)
                    .ThenInclude(pp => pp.Produk)
                  .FirstAsync(p => p.Id == pesanan.Id);

            return result;
        }

        public async Task<Pesanan?> CheckoutAsync(int pesananId, string metodePembayaran)
        {
            var pesanan = await context.Pesanan
                .Include(p => p.ProdukPesanan)
                .ThenInclude(pp => pp.Produk)
                .FirstOrDefaultAsync(p => p.Id == pesananId);

            if (string.IsNullOrEmpty(pesanan!.Status)) pesanan.Status = "Pending";

            if (pesanan == null || pesanan.Status != "Pending")
                return null;

            //Update
            pesanan.TotalHarga = pesanan.ProdukPesanan.Sum(p => p.Produk!.Harga * p.Jumlah);
            pesanan.MetodePembayaran = metodePembayaran;
            pesanan.Status = "Paid";

            await context.SaveChangesAsync();
            return pesanan;
        }

        public async Task<Pesanan?> CancelAsync(int pesananId)
        {
            var pesanan = await context.Pesanan.FirstOrDefaultAsync(p => p.Id == pesananId);

            if (string.IsNullOrEmpty(pesanan!.Status)) pesanan.Status = "Pending";

            if (pesanan == null || pesanan.Status != "Pending") return null;

            pesanan.Status = "Canceled";
            await context.SaveChangesAsync();
            return pesanan;
        }

        public async Task<IEnumerable<Pesanan>> GetHistoryAsync(string? status = null, DateTime? from = null, DateTime? to = null)
        {
            var query = context.Pesanan
                .Include(p => p.ProdukPesanan)
                .ThenInclude(pp => pp.Produk)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(p => p.Status == status);

            if (from.HasValue)
                query = query.Where(p => p.TanggalPesan >= from.Value);

            if (to.HasValue)
            {
                var endOfDay = to.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(p => p.TanggalPesan <= endOfDay);
            }
                

            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }
    }
}
