using CumpleMVC.Data;
using CumpleMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumpleMVC.Controllers
{
    [ApiController]
    [Route("api/mensajesEspeciales")]  // ← Aquí va la ruta
    public class MensajesEspecialesApiCtrl : ControllerBase
    {
        private readonly AppDbContext _context;

        public MensajesEspecialesApiCtrl(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("todos")]
        public IActionResult Todos()
        {
            var mensajes = _context.MensajesEspeciales
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new 
                {
                    m.Id,
                    m.Autor,
                    m.Contenido,
                    m.CreatedAt
                })
                .ToList();

            return Ok(mensajes);
        }

        [HttpPost("enviar")]
        public IActionResult Enviar([FromBody] MensajeEspecial mensaje)
        {
            if (mensaje == null || string.IsNullOrWhiteSpace(mensaje.Contenido))
                return BadRequest(new { success = false, message = "El mensaje no puede estar vacío" });

            mensaje.CreatedAt = DateTime.UtcNow;
            _context.MensajesEspeciales.Add(mensaje);
            _context.SaveChanges();

            return Ok(new { success = true });
        }
    }
}