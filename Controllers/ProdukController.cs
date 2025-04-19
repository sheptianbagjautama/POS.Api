using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class ProdukController : ControllerBase
    {
        private readonly IProdukRepository produkRepository;
        private readonly IMapper mapper;

        public ProdukController(IProdukRepository produkRepository, IMapper mapper)
        {
            this.produkRepository = produkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produk = await produkRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<ProdukDto>>(produk)); //Map to DTO
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produk = await produkRepository.GetByIdAsync(id);
            if (produk == null) return NotFound();

            return Ok(mapper.Map<ProdukDto>(produk)); //Map to DTO
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProdukDto dto)
        {
            var produk = mapper.Map<Produk>(dto); // Map to Entity
            var result = await produkRepository.CreateAsync(produk);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<ProdukDto>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProdukDto dto)
        {
            var produk = await produkRepository.GetByIdAsync(id);
            if (produk == null) return NotFound();

            var updateProduk = mapper.Map(dto, produk); // Map to Entity

            var updated = await produkRepository.UpdateAsync(updateProduk);
            if (!updated) return BadRequest("Update Gagal");

            return Ok("Update berhasil");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await produkRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("Produk dihapus");
        }
    }
}
