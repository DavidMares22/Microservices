using Servicios.Api.Libreria.Core.Entities;

namespace Servicios.Api.Libreria.Repository
{
    public interface IMongoRepository<T> where T : IDocument
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task<T> InsertDocument(T document);
        Task<T> UpdateDocument(T document);
        Task<bool> DeleteDocument(string id);
    }
}
