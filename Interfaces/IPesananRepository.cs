using POS.Api.Entities;

namespace POS.Api.Interfaces
{
    public interface IPesananRepository
    {
        Task<IEnumerable<Pesanan>> GetAllAsync();
        Task<Pesanan?> GetByIdAsync(int id);
        Task<Pesanan> CreateAsync(Pesanan pesanan);
    }
}
