using Microsoft.AspNetCore.Identity;
using POS.Api.Interfaces;
using POS.Api.Models;

namespace POS.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            // This method retrieves all users from the database using the UserManager.
            return await Task.FromResult(userManager.Users.ToList());
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

    }
}
