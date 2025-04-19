using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.DTOs;
using POS.Api.Entities;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MejaController : ControllerBase
    {
        private readonly IMejaRepository mejaRepository;
        private readonly IMapper mapper;

        public MejaController(IMejaRepository mejaRepository, IMapper mapper)
        {
            this.mejaRepository = mejaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meja = await mejaRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<MejaDto>>(meja));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var meja = await mejaRepository.GetByIdAsync(id);
            if (meja == null) return NotFound();

            return Ok(mapper.Map<MejaDto>(meja));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMejaDto dto)
        {
            var meja = mapper.Map<Meja>(dto);
            var result = await mejaRepository.CreateAsync(meja);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, mapper.Map<MejaDto>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMejaDto dto)
        {
            var meja = await mejaRepository.GetByIdAsync(id);
            if (meja == null) return NotFound();

            var updateMeja = mapper.Map(dto, meja);

            var updated = await mejaRepository.UpdateAsync(updateMeja);
            if (!updated) return BadRequest("Update gagal");

            return Ok("Update berhasil");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await mejaRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("Meja dihapus");
        }

    }
}
