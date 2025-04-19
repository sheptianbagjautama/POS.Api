using POS.Api.Entities;

namespace POS.Api.Interfaces
{
    public interface IKategoriRepository
    {
        Task<IEnumerable<Kategori>> GetAllAsync();
        Task<Kategori?> GetByIdAsync(int id);
        Task<Kategori> CreateAsync(Kategori kategori);
        Task<bool> UpdateAsync(Kategori kategori);
        Task<bool> DeleteAsync(int id);
    }
}
