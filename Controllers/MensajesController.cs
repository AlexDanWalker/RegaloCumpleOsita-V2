using CumpleMVC.Models;
using CumpleMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace CumpleMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensajesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MensajesController(AppDbContext context)
        {
            _context = context;
        }

        // 1️⃣ Enviar mensaje
        [HttpPost("enviar")]
        public IActionResult Enviar([FromBody] TeExtrano mensaje)
        {
            mensaje.CreatedAt = DateTime.UtcNow; // registrar fecha
            _context.TeExtranos.Add(mensaje);
            _context.SaveChanges();
            return Ok(new { success = true });
        }

        // 2️⃣ Contadores diarios y totales
        [HttpGet("contar")]
        public IActionResult Contar()
        {
            var hoy = DateTime.UtcNow.Date; // usar UTC para consistencia
            var data = new
            {
                hoyOsita = _context.TeExtranos.Count(m => m.De == "osita" && m.CreatedAt.Date == hoy),
                totalOsita = _context.TeExtranos.Count(m => m.De == "osita"),
                hoyGordito = _context.TeExtranos.Count(m => m.De == "gordito" && m.CreatedAt.Date == hoy),
                totalGordito = _context.TeExtranos.Count(m => m.De == "gordito")
            };
            return Ok(data);
        }

        // 3️⃣ Traer todos los mensajes (para el historial)
        [HttpGet("todos")]
        public IActionResult Todos()
        {
            var todos = _context.TeExtranos
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
            return Ok(todos);
        }
    }
}