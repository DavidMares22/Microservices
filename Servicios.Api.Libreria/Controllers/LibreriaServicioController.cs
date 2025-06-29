using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.Api.Libreria.Core.Entities;
using Servicios.Api.Libreria.Repository;

namespace Servicios.Api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {

        private readonly IMongoRepository<AutorEntity> _autorMongoRepository;

        private readonly IAutorRepository _autorRepository;

        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorMongoRepository)
        {
            _autorRepository = autorRepository;
            _autorMongoRepository = autorMongoRepository;
        }

        [HttpGet("autoresGenerico")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutoresGenerico()
        {

            var autores = await _autorMongoRepository.GetAll();

            return Ok(autores);
        }
        
        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores() {

            var autores = await _autorRepository.GetAutores();

            return Ok(autores);
        }


    }
}
