using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Contracts.Domains
{
    public abstract class MongoEntity : IAuditable
    {
        [BsonId()]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("createdDate")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTimeOffset CreatedDate { get; set; }
        [BsonElement("lastModifiedDate")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}