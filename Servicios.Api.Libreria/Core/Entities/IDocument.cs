using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Servicios.Api.Libreria.Core.Entities
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedDate { get; }

    }
}
