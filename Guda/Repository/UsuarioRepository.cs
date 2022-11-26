using Guda.Data;
using Guda.Models;
using Guda.Repository.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection.Metadata;

namespace Guda.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool ExisteUsuario(string nombre)
        {
            if (_db.Usuarios.Any(c=>c.UsuarioAcceso == nombre))
            {
                return true;
            }
            return false;
        }

        public Usuario GetUsuario(int id)
        {
            return _db.Usuarios.FirstOrDefault(c=>c.Id == id);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _db.Usuarios.OrderBy(c=>c.UsuarioAcceso).ToList();   
        }

        public bool Guardar()
        {
            return _db.SaveChanges()>=0 ? true: false;
        }

        public Usuario Login(string nombre, string password)
        {
            var user = _db.Usuarios.FirstOrDefault(c=>c.UsuarioAcceso == nombre);

            if (user == null) 
            {
                return null;
            }
            if (!VerificaPasswordHash(password,user.PasswordHash, user.PasswordSalt))
            {
               return null;
            }

            return user;
        }

        public Usuario Registro(Usuario usuario, string password)
        {
            byte[] passHash, passSalt;
            CrearPasswordHash(password, out passHash, out passSalt);

            usuario.PasswordHash = passHash;
            usuario.PasswordSalt = passSalt;

            _db.Usuarios.Add(usuario);
            Guardar();

            return usuario;
        }
        private void CrearPasswordHash(string pass, out byte[] passHash,out  byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));

            }
            
        }


        private bool VerificaPasswordHash(string pass, byte[] passHash, byte[]passSalt )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passHash[i]) return false;
                }
            
            }
            return true;
        }
    }
}
