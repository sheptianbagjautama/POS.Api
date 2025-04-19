using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Entities;
using POS.Api.Interfaces;
using System.Security.Claims;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesananController : ControllerBase
    {
        private readonly IPesananRepository pesananRepository;
        private readonly IMapper mapper;

        public PesananController(IPesananRepository pesananRepository, IMapper mapper)
        {
            this.pesananRepository = pesananRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await pesananRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<PesananDto>>(list));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pesanan = await pesananRepository.GetByIdAsync(id);
            if (pesanan == null) return NotFound();
            return Ok(mapper.Map<PesananDto>(pesanan));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePesananDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pesanan = mapper.Map<Pesanan>(dto);
            pesanan.UserId = userId;

            var created = await pesananRepository.CreateAsync(pesanan);
            return Ok(mapper.Map<PesananDto>(created));
        }
    }
}
