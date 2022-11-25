using System.ComponentModel.DataAnnotations;
using static Guda.Models.Articulo;

namespace Guda.Models.DTOs
{
    public class ArticuloDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre  es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string RutaImagen { get; set; }

        public string Duracion { get; set; }
        public TipoClasificacion Clasificacion { get; set; }

        public int categoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}
