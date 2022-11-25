using AutoMapper;
using Guda.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IArticuloRepository artRepo;
        private readonly IMapper mapper;

        public ArticulosController(IArticuloRepository artRepo,IMapper mapper)
        {
            this.artRepo = artRepo;
            this.mapper = mapper;
        }
    }
}
