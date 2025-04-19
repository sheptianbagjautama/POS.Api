using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriRepository kategoriRepository;
        private readonly IMapper mapper;

        public KategoriController(IKategoriRepository kategoriRepository, IMapper mapper)
        {
            this.kategoriRepository = kategoriRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var kategori = await kategoriRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<KategoriDto>>(kategori)); // Map to DTO
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var kategori = await kategoriRepository.GetByIdAsync(id);
            if (kategori == null) return NotFound();

            return Ok(mapper.Map<KategoriDto>(kategori)); // Map to DTO
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKategoriDto dto)
        {
            var kategori = mapper.Map<Kategori>(dto); // Map to Entity
            var result = await kategoriRepository.CreateAsync(kategori);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<KategoriDto>(result)); // Map to DTO
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateKategoriDto dto)
        {
            var kategori = await kategoriRepository.GetByIdAsync(id);
            if (kategori == null) return NotFound();

            kategori.Nama = dto.Nama;
            kategori.Deskripsi = dto.Deskripsi;

            var updated = await kategoriRepository.UpdateAsync(kategori);
            if (!updated) return BadRequest("Update Gagal");

            return Ok("Update berhasil");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await kategoriRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("Kategori dihapus");
        }
    }
}
