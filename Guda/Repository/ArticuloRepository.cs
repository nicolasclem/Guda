using Guda.Data;
using Guda.Models;
using Guda.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Guda.Repository
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool BorrarArticulo(Articulo articulo)
        {
            _db.Articulos.Remove(articulo);
            return Guardar();
        }

        public IEnumerable<Articulo> BuscarArticulo(string nombre)
        {
            IQueryable<Articulo> query = _db.Articulos;
            if(!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
                
            }
            return query.ToList();
        }

        public bool CrearArticulo(Articulo articulo)
        {
            _db.Articulos.Add(articulo);
            return Guardar();
        }

        public bool EditarArticulo(Articulo articulo)
        {
            _db.Articulos.Update(articulo);
            return Guardar();
        }

        public bool ExisteArticulo(string nombre)
        {
            bool valor = _db.Articulos.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        
        }

        public bool ExisteArticulo(int id)
        {
            bool valor = _db.Articulos.Any(c => c.Id == id);
            return valor;
        }

        public Articulo GetArticulo(int id)
        {
            return _db.Articulos.FirstOrDefault(c=> c.Id ==id);
        }

        public ICollection<Articulo> GetArticuloEnCategoria(int catId)
        {
            return _db.Articulos.Include(ca => ca.Categoria).Where(c => c.categoriaId == catId).ToList();
        }
        
        public ICollection<Articulo> GetArticulos()
        {
            return _db.Articulos.OrderBy(c=>c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _db.SaveChanges() >=0 ? true: false;
        }
    }
}
