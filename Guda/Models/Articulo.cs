using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace Guda.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre  es obligatorio")]

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string RutaImagen { get; set; }

        public string Duracion { get; set; }
        public enum TipoClasificacion { Siete,Dieciseis,Dieciocho }
        public TipoClasificacion Clasificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria Categoria { get; set; }
    }
}
