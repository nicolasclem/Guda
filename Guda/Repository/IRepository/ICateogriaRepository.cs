using Guda.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guda.Repository.IRepository
{
    public interface ICateogriaRepository
    {
        ICollection<Categoria> GetCategorias();

        Categoria GetCategoria(int id);

        bool ExisteCategoria(string nombre);

        bool ExisteCategoria(int  id);

        bool CrearCategoria(Categoria categoria);

        bool EditarCategoria(Categoria categoria);

        bool BorrarCategoria(Categoria categoria);

        bool Guardar();

    }
}
