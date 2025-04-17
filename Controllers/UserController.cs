using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Interfaces;
using POS.Api.Models;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository,
            IRoleRepository roleRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAllUsersAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var dto = mapper.Map<UserDto>(users);
                dto.Roles = await userRepository.GetUserRolesAsync(user); //add roles to the DTO
                userDtos.Add(dto);
            }

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var dto = mapper.Map<UserDto>(user);
            dto.Roles = await userRepository.GetUserRolesAsync(user); //add roles to the DTO

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var created = await userRepository.CreateUserAsync(user, dto.Password);
            if (!created) return BadRequest("Failed to create user");

            if (dto.Roles != null)
                await userManager.AddToRolesAsync(user, dto.Roles);

            return Ok("User created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateUserDto dto)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.FullName = dto.FullName;

            var updated = await userRepository.UpdateUserAsync(user);
            if (!updated) return BadRequest("Failed to update user");

            if (dto.Roles != null)
            {
                var currentRoles = await userRepository.GetUserRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, currentRoles);
                await userManager.AddToRolesAsync(user, dto.Roles);
            }

            return Ok("User updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await userRepository.DeleteUserAsync(id);
            if (!success) return NotFound();

            return Ok("User deleted");
        }
    }
}
