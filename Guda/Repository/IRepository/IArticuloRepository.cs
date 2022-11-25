using Guda.Models;

namespace Guda.Repository.IRepository
{
    public interface IArticuloRepository
    {
        ICollection<Articulo> GetArticulos();
        ICollection<Articulo> GetArticuloEnCategoria(int catId);

        Articulo GetArticulo(int id);

        IEnumerable<Articulo> BuscarArticulo(string nombre);

        bool ExisteArticulo(string nombre);

        bool ExisteArticulo(int id);

        bool CrearArticulo(Articulo articulo);

        bool EditarArticulo(Articulo articulo);

        bool BorrarArticulo(Articulo articulo);

        bool Guardar();
    }
}
