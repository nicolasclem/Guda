using Guda.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Guda.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        //Agregar los modelos 
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Articulo> Articulos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
