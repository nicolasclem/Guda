using AutoMapper;
using Guda.Models;
using Guda.Models.DTOs;
using Guda.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Guda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository userRepo;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UsuariosController(IUsuarioRepository userRepo, IMapper mapper , IConfiguration config)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.config = config;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = userRepo.GetUsuarios();

            var listaUsuariosDTO = new List<UsuarioDTO>();

            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDTO.Add(mapper.Map<UsuarioDTO>(lista));
            }

            return Ok(listaUsuariosDTO);
        
        }

        [HttpGet("{id:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int id)
        {
            var itemUsuario = userRepo.GetUsuario(id);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDTO = mapper.Map<UsuarioDTO>(itemUsuario);

            return Ok(itemUsuarioDTO);
        }
        [AllowAnonymous]
        [HttpPost("Registro")]
        public IActionResult Registro(UsuarioAuthDTO usuarioAuthDTO)
        {
            usuarioAuthDTO.Usuario = usuarioAuthDTO.Usuario.ToLower();
            if (userRepo.ExisteUsuario(usuarioAuthDTO.Usuario))
            {
                return BadRequest("El usuario ya fue registrado!");
            }

            var usuarioACrear = new Usuario
            {
                UsuarioAcceso = usuarioAuthDTO.Usuario
            };

            var usuarioCreado = userRepo.Registro(usuarioACrear, usuarioAuthDTO.Password);

            return Ok(usuarioCreado);
            


        }
         [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioAuthLoginDTO usuarioAuthLoginDTO)
        {
            
            var usuarioDesdeRepo =  userRepo.Login(usuarioAuthLoginDTO.Usuario, usuarioAuthLoginDTO.Password);
            if (usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, usuarioAuthLoginDTO.Usuario.ToString())
            };

            // genrar el token 

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var credenciales = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials= credenciales
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token=tokenHandler.WriteToken(token)

            });        
        }
    }
}
