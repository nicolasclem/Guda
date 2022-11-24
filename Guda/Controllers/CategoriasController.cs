using AutoMapper;
using Guda.Models;
using Guda.Models.DTOs;
using Guda.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICateogriaRepository catRepo;
        private readonly IMapper mapper;

        public CategoriasController(ICateogriaRepository catRepo, IMapper mapper)
        {
            this.catRepo = catRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCateogrias()
        {
            var listaCategorias = catRepo.GetCategorias();

            var listaCategoriaDTO = new List<CategoriaDTO>();
            foreach( var lista in listaCategorias)
            {
                listaCategoriaDTO.Add(mapper.Map<CategoriaDTO>(lista));
            }

            
            return Ok(listaCategoriaDTO);
        }
        [HttpGet("{id:int}", Name ="GetCategoria")]
        public IActionResult GetCategoria(int id)
        {
            var itemCategoria = catRepo.GetCategoria(id);
            if(itemCategoria == null)
            {
                return NotFound();
            }
            var itemCategoriaDTO = mapper.Map<CategoriaDTO>(itemCategoria);

            return Ok(itemCategoriaDTO);
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody]CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
            {
                return BadRequest(ModelState);
            }
            if(catRepo.ExisteCategoria(categoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La categoria ya Existe");
                return StatusCode(404, ModelState);
            }
            var categoria = mapper.Map<Categoria>(categoriaDTO);

            if (!catRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal en el registro {categoria.Nombre}");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpPatch("{id:int}")]
        public IActionResult EditarCategoria(int id, [FromBody]CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO == null || id != categoriaDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var categoria = mapper.Map<Categoria>(categoriaDTO);
            if (!catRepo.EditarCategoria(categoria))
            {
                ModelState.AddModelError("", $"algo salio mal en la actualizacion {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult BorrarCategoria(int id) 
        {
            if (!catRepo.ExisteCategoria(id))
            {
                return NotFound();  
            }
            var categoria=  catRepo.GetCategoria(id);

            if (!catRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"algo salio mal en la Eliminacion de {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent(); 

        }
    }
}
