using System.ComponentModel.DataAnnotations;

namespace Guda.Models.DTOs
{
    public class UsuarioAuthLoginDTO
    {
        [Required(ErrorMessage = "Nombre de  usuario Obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Password  de  usuario Obligatorio")]
        public string Password { get; set; }
    }
}
