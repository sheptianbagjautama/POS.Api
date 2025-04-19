using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Repositories
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly ApplicationDbContext context;

        public KategoriRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Kategori>> GetAllAsync()
        {
            return await context.Kategori.ToListAsync();
        }

        public async Task<Kategori?> GetByIdAsync(int id)
        {
            return await context.Kategori.FindAsync(id);
        }

        public async Task<Kategori> CreateAsync(Kategori kategori)
        {
            context.Kategori.Add(kategori);
            await context.SaveChangesAsync();
            return kategori;
        }

        public async Task<bool> UpdateAsync(Kategori kategori)
        {
            context.Kategori.Update(kategori);
            return await context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var kategori = await context.Kategori.FindAsync(id);
            if (kategori == null) return false;

            context.Kategori.Remove(kategori);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
