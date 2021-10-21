using System.ComponentModel.DataAnnotations;

namespace Chulygon.Models
{
    public class Comando
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ComoSeFai { get; set; }
        [Required]
        public string Linea { get; set; }
        [Required]
        public string Plataforma { get; set; }
    }
}