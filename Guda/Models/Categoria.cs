using System.ComponentModel.DataAnnotations;

namespace Guda.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string  Nombre { get; set; }
    }
}
