using System.ComponentModel.DataAnnotations;

namespace Guda.Models.DTOs
{
    public class UsuarioAuthDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre de  usuario Obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Password  de  usuario Obligatorio")]
        [StringLength(10,MinimumLength =4,ErrorMessage ="Contraseña entre  10 y4  caracteres")]
        public string Password { get; set; }
    }
}
