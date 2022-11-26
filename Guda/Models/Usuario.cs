using System.ComponentModel.DataAnnotations;

namespace Guda.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UsuarioAcceso { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
