using Servicios.Api.Libreria.Core.Entities;

namespace Servicios.Api.Libreria.Repository
{
    public interface IMongoRepository<T> where T : IDocument
    {
        Task<IEnumerable<T>> GetAll();
    }
}
