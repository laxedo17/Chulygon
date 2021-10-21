using System.ComponentModel.DataAnnotations;

namespace Chulygon.Dtos
{
    public class ComandoActualizarDto
    {
        //public int Id { get; set; } 
        //Id non fai falta aqui porque realmente creaa de maneira automatica a base de datos
        [Required]
        public string ComoSeFai { get; set; }

        [Required]
        public string Linea { get; set; }
        [Required]
        public string Plataforma { get; set; }
    }
}