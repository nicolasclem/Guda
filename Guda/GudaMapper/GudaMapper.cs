using AutoMapper;
using Guda.Models;
using Guda.Models.DTOs;

namespace Guda.GudaMapper
{
    public class GudaMapper: Profile
    {
        public GudaMapper()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Articulo, ArticuloDTO>().ReverseMap();
            CreateMap<Articulo, ArticuloCreateDTO>().ReverseMap();
            CreateMap<Articulo, ArticuloUpDateDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioAuthDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioAuthLoginDTO>().ReverseMap();
        }
    }
}
