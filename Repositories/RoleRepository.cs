using Microsoft.AspNetCore.Identity;
using POS.Api.Interfaces;
using POS.Api.Models;

namespace POS.Api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationRole>> GetAllRolesAsync()
        {
            return await Task.FromResult(roleManager.Roles.ToList());
        }

        public async Task<ApplicationRole?> GetRoleByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (await roleManager.RoleExistsAsync(roleName))
                return false;

            var role = new ApplicationRole { Name = roleName };
            var result = await roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null) return false;

            var result = await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
    }
}
