namespace Guda.Models.DTOs
{
    public class UsuarioDTO
    {
        public string UsuarioAcceso { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
