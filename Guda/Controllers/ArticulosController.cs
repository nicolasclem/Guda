using AutoMapper;
using Guda.Models;
using Guda.Models.DTOs;
using Guda.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Guda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepository artRepo;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ArticulosController(IArticuloRepository artRepo, IMapper mapper,IWebHostEnvironment hostingEnvironment)
        {
            this.artRepo = artRepo;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetArticulos()
        {
            var listaArticulos = artRepo.GetArticulos();

            var listaArticulosDTO = new List<ArticuloDTO>();

            foreach (var lista in listaArticulos)
            {
                listaArticulosDTO.Add(mapper.Map<ArticuloDTO>(lista));

            }
            return Ok(listaArticulosDTO);
        }
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetArticulo")]
        public IActionResult GetArticulo(int id)
        {
            var itemArticulo = artRepo.GetArticulo(id);
            if (itemArticulo == null)
            {
                return NotFound();
            }
            var itemArticuloDTO = mapper.Map<ArticuloDTO>(itemArticulo);

            return Ok(itemArticuloDTO);
        }
        [HttpPost]
        public IActionResult CrearArticulo([FromForm] ArticuloCreateDTO articuloDTO)
        {
            if (articuloDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (artRepo.ExisteArticulo(articuloDTO.Nombre!))
            {
                ModelState.AddModelError("", "el articulo ya existe");
                return StatusCode(404, ModelState);
            }
            /* subida de archivos*/
            var archivo = articuloDTO.Foto;
            string rutaPrincipal = hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;
            if(archivo == null)
            {
                articuloDTO.RutaImagen = @"\Fotos\Imagen_no_disponible.png";
            }

            if (archivo != null && archivo.Length > 0)
            {
                //Nueva imagen
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"Fotos");
                var extension = Path.GetExtension(archivos[0].FileName);


                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create)) 
                {
                    archivos[0].CopyTo(fileStreams);
                }
                articuloDTO.RutaImagen = @"\Fotos\" + nombreFoto + extension;
            }
            
            var articulo = mapper.Map<Articulo>(articuloDTO);
            if (!artRepo.CrearArticulo(articulo))
            {
                ModelState.AddModelError("", $"Algo salio  mal al intentar guardar {articulo.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetArticulo", new { id = articulo.Id }, articulo);
        }

        [AllowAnonymous]
        [HttpGet("GetArticuloEnCategoria/{id:int}")]
        public IActionResult GetArticuloEnCategoria(int id)
        {
            var listaArticulo = artRepo.GetArticuloEnCategoria(id);
            if(listaArticulo == null)
            {
                return NotFound();
            }

            var itemArticulo= new List<ArticuloDTO>();
            foreach (var item in listaArticulo)
            {
                itemArticulo.Add(mapper.Map<ArticuloDTO>(item));
            }
             return Ok(itemArticulo);
        }
        [AllowAnonymous]
        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = artRepo.BuscarArticulo(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error  recuperando datos de la aplicacion");
            }
        }
        [HttpPatch("{id:int}")]
        public IActionResult EditarArticulo(int id, [FromBody] ArticuloUpDateDTO articuloDTO)
        {
            if (articuloDTO == null || id != articuloDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var articulo = mapper.Map<Articulo>(articuloDTO);
            if (!artRepo.EditarArticulo(articulo))
            {
                ModelState.AddModelError("", $"algo salio mal en la actualizacion {articulo.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult BorrarCategoria(int id)
        {
            if (!artRepo.ExisteArticulo(id))
            {
                return NotFound();
            }
            var articulo = artRepo.GetArticulo(id);

            if (!artRepo.BorrarArticulo(articulo))
            {
                ModelState.AddModelError("", $"algo salio mal en la Eliminacion de {articulo.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}