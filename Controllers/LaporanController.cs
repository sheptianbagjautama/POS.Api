using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class LaporanController : ControllerBase
    {
        private readonly ILaporanRepository laporanRepository;

        public LaporanController(ILaporanRepository laporanRepository)
        {
            this.laporanRepository = laporanRepository;
        }

        [HttpGet("penjualan")]
        public async Task<IActionResult> GetLaporanPenjualan([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string? userId)
        {
            if (from > to) return BadRequest("Tanggal tidak valid");

            var laporan = await laporanRepository.GetLaporanPenjualanAsync(from, to, userId);
            return Ok(laporan);
        }

        [HttpGet("produk-terlaris")]
        public async Task<IActionResult> GetProdukTerlaris(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] int? top =5)
        {
            if (from > to) return BadRequest("Tanggal tidka valid");

            var result = await laporanRepository.GetProdukTerlarisAsync(from, to, top);
            return Ok(result);
        }
    }
}
