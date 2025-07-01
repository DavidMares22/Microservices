
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using Servicios.Api.Libreria.Core;
using Servicios.Api.Libreria.Core.Entities;

namespace Servicios.Api.Libreria.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IDocument
    {

        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);

            _collection = db.GetCollection<T>(GetCollectionName(typeof(T)));

        }

        private protected string GetCollectionName(Type documentType)
        {
            var attribute = documentType
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault() as BsonCollectionAttribute;

            if (attribute == null)
            {
                throw new InvalidOperationException($"The type {documentType.Name} does not have a BsonCollectionAttribute.");
            }

            return attribute.CollectionName;
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_ => true)
                                     .ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter)
                              .FirstOrDefaultAsync();
        }

        public async Task<T> InsertDocument(T document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), "Document cannot be null");
            }

            await _collection.InsertOneAsync(document);
            return document;
        }

        public async Task<T> UpdateDocument(T document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document), "Document cannot be null");
            }

            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            var result = await _collection.ReplaceOneAsync(filter, document);

            if (result.MatchedCount == 0)
            {
                throw new KeyNotFoundException($"Document with ID {document.Id} not found.");
            }

            return document;

        }

        public Task<bool> DeleteDocument(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty", nameof(id));
            }

            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            var result = _collection.DeleteOne(filter);

            return Task.FromResult(result.DeletedCount > 0);
        }

        public async Task<PaginationEntity<T>> PaginationBy(Expression<Func<T, bool>> filterExpression, PaginationEntity<T> pagination)
        {

            var sort = Builders<T>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection == "desc")
            {
                sort = Builders<T>.Sort.Descending(pagination.Sort);
            }

            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(p => true)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)
                                   .Limit(pagination.PageSize)
                                   .ToListAsync();
            }
            else
            {

                pagination.Data = await _collection.Find(filterExpression)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)
                                   .Limit(pagination.PageSize)
                                   .ToListAsync();
            }


            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
            var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalDocuments / pagination.PageSize)));

            pagination.PagesQuantity = totalPages;

            return pagination;
        }

        public async Task<PaginationEntity<T>> PaginationByFilter(PaginationEntity<T> pagination)
        {
            var sort = Builders<T>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection == "desc")
            {
                sort = Builders<T>.Sort.Descending(pagination.Sort);
            }


            var totalDocuments = 0;
            if (pagination.FilterValue == null)
            {
                pagination.Data = await _collection.Find(p => true)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)
                                   .Limit(pagination.PageSize)
                                   .ToListAsync();


                totalDocuments = (await _collection.Find(p => true).ToListAsync()).Count();
            }
            else
            {

                var valueFilter = ".*" + pagination.FilterValue.Valor + ".*";
                var filter = Builders<T>.Filter.Regex(pagination.FilterValue.Propiedad, new BsonRegularExpression(valueFilter, "i"));

                pagination.Data = await _collection.Find(filter)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)
                                   .Limit(pagination.PageSize)
                                   .ToListAsync();

                totalDocuments = (await _collection.Find(filter).ToListAsync()).Count();


            }
            //libro = 1000
            //long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);

            ///libro = 56

            var rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(pagination.PageSize));

            var totalPages = Convert.ToInt32(rounded);

            pagination.PagesQuantity = totalPages;
            pagination.TotalRows = Convert.ToInt32(totalDocuments);


            return pagination;
        }
    }
}
