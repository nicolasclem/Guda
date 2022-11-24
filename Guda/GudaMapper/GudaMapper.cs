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
        }
    }
}
