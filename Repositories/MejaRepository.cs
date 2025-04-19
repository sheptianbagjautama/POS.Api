using Microsoft.EntityFrameworkCore;
using POS.Api.Data;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Repositories
{
    public class MejaRepository : IMejaRepository
    {
        private readonly ApplicationDbContext context;

        public MejaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Meja>> GetAllAsync()
        {
           return await context.Meja.ToListAsync();
        }

        public async Task<Meja?> GetByIdAsync(int id)
        {
            return await context.Meja.FindAsync(id);
        }

        public async Task<Meja> CreateAsync(Meja meja)
        {
            context.Meja.Add(meja);
            await context.SaveChangesAsync();
            return meja;
        }

        public async Task<bool> UpdateAsync(Meja meja)
        {
            context.Meja.Update(meja);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var meja = await context.Meja.FindAsync(id);
            if (meja == null) return false;

            context.Meja.Remove(meja);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
