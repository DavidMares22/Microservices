﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Servicios.Api.Libreria.Core.Entities
{
    public class Document : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDate => DateTime.UtcNow;
    }


}
