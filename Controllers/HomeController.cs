using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CumpleMVC.Models;
using CumpleMVC.Data; // ✅ Importante

namespace CumpleMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // ✅ DbContext

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Inicio";
            return View();
        }

        public IActionResult TeExtrano()
        {
            ViewData["Title"] = "Te extraño";
            return View();
        }

        public IActionResult TuOsito()
        {
            ViewData["Title"] = "Tu Osito";
            return View();
        }

        public IActionResult Galeria()
        {
            ViewData["Title"] = "Galería";
            return View();
        }

        // ✅ Aquí pasamos los mensajes especiales a la vista
        public IActionResult MensajesEspeciales()
        {
            ViewData["Title"] = "Mensajes Especiales";

            var mensajes = _context.MensajesEspeciales
                .OrderByDescending(m => m.CreatedAt)
                .ToList();

            return View(mensajes ?? new List<MensajeEspecial>());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}