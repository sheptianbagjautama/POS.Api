using POS.Api.Entities;

namespace POS.Api.Interfaces
{
    public interface IMejaRepository
    {
        Task<IEnumerable<Meja>> GetAllAsync();
        Task<Meja?> GetByIdAsync(int id);
        Task<Meja> CreateAsync(Meja meja);
        Task<bool> UpdateAsync(Meja meja);
        Task<bool> DeleteAsync(int id);
    }
}
