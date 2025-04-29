using System.ComponentModel.DataAnnotations;

namespace CarreraAutonomiaAPI.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool EsMayorEdad { get; set; }
        public string Nacionalidad { get; set; }
        public string Identificacion { get; set; } // CUI o Pasaporte
        [Required]
        public int Categoria { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
