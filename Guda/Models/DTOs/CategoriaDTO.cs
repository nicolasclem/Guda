using System.ComponentModel.DataAnnotations;

namespace Guda.Models.DTOs
{
    public class CategoriaDTO
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string  Nombre { get; set; }
    }
}
