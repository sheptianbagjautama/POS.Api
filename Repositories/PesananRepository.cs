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
    }
}
