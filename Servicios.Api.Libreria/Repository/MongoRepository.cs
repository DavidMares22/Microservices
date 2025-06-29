
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
    }
}
