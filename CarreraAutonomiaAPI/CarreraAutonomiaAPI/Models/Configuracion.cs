using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarreraAutonomiaAPI.Models
{
    [Table("Configuraciones")]
    public class Configuraciones
    {
        [Key]
        public int Id { get; set; }

        [Column("limite_camisas")]
        public int limite_camisas { get; set; }
    }
}
