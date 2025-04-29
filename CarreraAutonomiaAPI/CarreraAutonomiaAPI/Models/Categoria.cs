using System.ComponentModel.DataAnnotations;

namespace CarreraAutonomiaAPI.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string nombre { get; set; }
        public bool activa { get; set; }
    }

}
