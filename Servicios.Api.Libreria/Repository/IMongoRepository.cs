using System.Linq.Expressions;
using Servicios.api.Libreria.Core.Entities;
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

        Task<PaginationEntity<T>> PaginationBy(
            Expression<Func<T, bool>> filterExpression,
            PaginationEntity<T> pagination
        );

        Task<PaginationEntity<T>> PaginationByFilter(
           PaginationEntity<T> pagination
       );
    }
}
