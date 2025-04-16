using POS.Api.Models;

namespace POS.Api.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<ApplicationRole>> GetAllRolesAsync();
        Task<ApplicationRole?> GetRoleByIdAsync(string id);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleId);
    }
}
