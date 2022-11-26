using Guda.Models;

namespace Guda.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario>GetUsuarios();
        Usuario GetUsuario(int id);
        bool ExisteUsuario(string nombre);
        Usuario Registro(Usuario usuario,string password);

        Usuario Login(string nombre,string password);

        bool Guardar();
    }
}
