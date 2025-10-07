
namespace CumpleMVC.Models
{
    public class TeExtrano
    {
        public int Id { get; set; }
        public string De { get; set; } = "gordito";
        public string Para { get; set; } = "osita";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}