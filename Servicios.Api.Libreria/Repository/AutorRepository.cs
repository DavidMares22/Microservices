using MongoDB.Driver;
using Servicios.Api.Libreria.Core.ContextMongoDB;
using Servicios.Api.Libreria.Core.Entities;

namespace Servicios.Api.Libreria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IAutorContext _autorContext;
        public AutorRepository(IAutorContext autorContext)
        {
            _autorContext = autorContext;
            
        }
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            var res = await _autorContext.Autores.Find(p => true).ToListAsync();
            return res;
        }
    }
}
