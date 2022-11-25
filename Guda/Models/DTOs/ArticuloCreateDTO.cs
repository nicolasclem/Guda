using static Guda.Models.Articulo;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Guda.Models.DTOs
{
    public class ArticuloCreateDTO
    {
        [Required(ErrorMessage ="El nombre  es obligatorio")]

        public string? Nombre { get; set; }
        
       
        public string? Descripcion { get; set; }
        
        public string? RutaImagen { get; set; }
        
        public IFormFile? Foto { get; set; }
        public string Duracion { get; set; }
        public TipoClasificacion Clasificacion { get; set; }

        public int categoriaId { get; set; }

    }
}
