using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OpenAIPoC.API.Domain.Common;

namespace OpenAIPoC.API.Domain.Competitions
{
    public class Competition : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required string Name { get; set; }
    }
}
