using CumpleMVC.Data;
using CumpleMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CumpleMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensajesEspecialesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MensajesEspecialesApiController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/MensajesEspeciales/enviar
        [HttpPost("enviar")]
        public IActionResult Enviar([FromBody] MensajeEspecial mensaje)
        {
            mensaje.CreatedAt = DateTime.UtcNow;
            _context.MensajesEspeciales.Add(mensaje);
            _context.SaveChanges();
            return Ok(new { success = true, mensaje });
        }

        // GET: /api/MensajesEspeciales/todos
        [HttpGet("todos")]
        public IActionResult Todos()
        {
            var mensajes = _context.MensajesEspeciales
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
            return Ok(mensajes);
        }
    }
}