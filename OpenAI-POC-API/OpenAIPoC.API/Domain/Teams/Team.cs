using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OpenAIPoC.API.Domain.Common;

namespace OpenAIPoC.API.Domain.Teams
{
    public class Team : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public required string Name { get; set; }
        public required List<Player> Squad { get; set; }
    }
}
