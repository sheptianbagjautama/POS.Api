using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await roleRepository.GetAllRolesAsync();
            return Ok(roles.Select(r => new {r.Id, r.Name})); // Select only Id and Name
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDto roleDto)
        {
            var success = await roleRepository.CreateRoleAsync(roleDto.Name);
            if (!success) return BadRequest("Role already exists or failed");

            return Ok("Role created");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await roleRepository.DeleteRoleAsync(id);
            if (!success) return NotFound("Role not found or failed to delete");

            return Ok("Role deleted");
        }


    }
}
