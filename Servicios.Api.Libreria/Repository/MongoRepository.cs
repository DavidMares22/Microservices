
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
    }
}
