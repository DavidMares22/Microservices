using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Libreria.Core.Entities;
using Servicios.Api.Libreria.Core.Entities;
using Servicios.Api.Libreria.Repository;

namespace Servicios.Api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaAutorController : ControllerBase
    {

        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;


        public LibreriaAutorController(IMongoRepository<AutorEntity> autorMongoRepository)
        {
            _autorGenericoRepository = autorMongoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAllAutors()
        {

            var autores = await _autorGenericoRepository.GetAll();

            return Ok(autores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetAutorById(string id)
        {
            var autor = await _autorGenericoRepository.GetById(id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }
        [HttpPost]
        public async Task<ActionResult<AutorEntity>> CreateAutor(AutorEntity autor)
        {
            if (autor == null)
            {
                return BadRequest("Autor cannot be null");
            }

            await _autorGenericoRepository.InsertDocument(autor);

            return CreatedAtAction(nameof(GetAutorById), new { id = autor.Id }, autor);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAutor(string id, AutorEntity autor)
        {
            if (id != autor.Id)
            {
                return BadRequest("Autor ID mismatch");
            }

            var existingAutor = await _autorGenericoRepository.GetById(id);
            if (existingAutor == null)
            {
                return NotFound();
            }

            await _autorGenericoRepository.UpdateDocument(autor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutor(string id)
        {
            var autor = await _autorGenericoRepository.GetById(id);
            if (autor == null)
            {
                return NotFound();
            }

            await _autorGenericoRepository.DeleteDocument(id);

            return NoContent();
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination(PaginationEntity<AutorEntity> pagination)
        {
            var resultado = await _autorGenericoRepository.PaginationBy(filter => filter.Nombre == pagination.Filter, pagination);
            
            return Ok(resultado);
            
        }

    }
}
