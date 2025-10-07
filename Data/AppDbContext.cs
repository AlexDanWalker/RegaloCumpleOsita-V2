using Microsoft.EntityFrameworkCore;
using CumpleMVC.Models;

namespace CumpleMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TeExtrano> TeExtranos { get; set; }
        public DbSet<MensajeEspecial> MensajesEspeciales { get; set; }
    }
}