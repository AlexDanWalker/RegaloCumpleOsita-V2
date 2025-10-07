namespace CumpleMVC.Models
{
    public class MensajeEspecial
    {
        public int Id { get; set; }
        public string Autor { get; set; } = ""; // osita o gordito
        public string Contenido { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}