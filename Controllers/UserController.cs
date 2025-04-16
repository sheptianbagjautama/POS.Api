using Microsoft.AspNetCore.Mvc;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await userRepository.DeleteUserAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
}
