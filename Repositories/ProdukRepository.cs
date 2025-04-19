using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Repositories
{
    public class ProdukRepository : IProdukRepository
    {
        private readonly ApplicationDbContext context;

        public ProdukRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Produk>> GetAllAsync()
        {
            return await context.Produk.ToListAsync();
        }

        public async Task<Produk?> GetByIdAsync(int id)
        {
            return await context.Produk.FindAsync(id);
        }

        public async Task<Produk> CreateAsync(Produk produk)
        {
            context.Produk.Add(produk);
            await context.SaveChangesAsync();
            return produk;
        }

        public async Task<bool> UpdateAsync(Produk produk)
        {
            context.Produk.Update(produk);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produk = await context.Produk.FindAsync(id);
            if (produk == null) return false;

            context.Produk.Remove(produk);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
