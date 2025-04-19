using POS.Api.Entities;

namespace POS.Api.Interfaces
{
    public interface IProdukRepository
    {
        Task<IEnumerable<Produk>> GetAllAsync();
        Task<Produk?> GetByIdAsync(int id);
        Task<Produk> CreateAsync(Produk produk);
        Task<bool> UpdateAsync(Produk produk);
        Task<bool> DeleteAsync(int id);
    }
}
