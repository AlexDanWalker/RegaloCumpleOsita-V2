using Microsoft.AspNetCore.Mvc;
using CumpleMVC.Data;
using CumpleMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CumpleMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeExtranoApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TeExtranoApiController(AppDbContext db)
        {
            _db = db;
        }

        // Registrar un "te extraño"
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeExtrano data)
        {
            if (string.IsNullOrEmpty(data.De) || string.IsNullOrEmpty(data.Para))
                return BadRequest("Faltan datos");

            var te = new TeExtrano { De = data.De, Para = data.Para, CreatedAt = DateTime.UtcNow };
            _db.TeExtranos.Add(te);
            await _db.SaveChangesAsync();

            return Ok(te);
        }

        // Obtener contadores por separado
        [HttpGet("contadores")]
        public async Task<IActionResult> GetContadores()
        {
            var today = DateTime.UtcNow.Date;

            // Contadores de osita para gordito
            var hoyDeOsita = await _db.TeExtranos.CountAsync(t =>
                t.De == "osita" && t.Para == "gordito" && t.CreatedAt >= today && t.CreatedAt < today.AddDays(1));
            var totalDeOsita = await _db.TeExtranos.CountAsync(t =>
                t.De == "osita" && t.Para == "gordito");

            // Contadores de gordito para osita
            var hoyDeGordito = await _db.TeExtranos.CountAsync(t =>
                t.De == "gordito" && t.Para == "osita" && t.CreatedAt >= today && t.CreatedAt < today.AddDays(1));
            var totalDeGordito = await _db.TeExtranos.CountAsync(t =>
                t.De == "gordito" && t.Para == "osita");

            return Ok(new
            {
                hoyDeOsita,
                totalDeOsita,
                hoyDeGordito,
                totalDeGordito
            });
        }
    }
}