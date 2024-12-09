using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OpenAIPoC.API.Domain.Common;
using OpenAIPoC.API.Domain.Teams;

namespace OpenAIPoC.API.Domain.Matches
{
    public class Match : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public required string HomeTeam { get; set; }

        public required string AwayTeam { get; set; }

        public required string Competition { get; set; }

        public required List<Player> HomeStarters { get; set; } = new List<Player>();

        public required List<Player> AwayStarters { get; set; } = new List<Player>();

        public required List<Player> HomeSubstitutes { get; set; } = new List<Player>();

        public required List<Player> AwaySubstitutes { get; set; } = new List<Player>();

        public required string PredictedWinner { get; set; }

        public required DateTime PredictionDate { get; set; }

        public required string HomeWinOdd { get; set; }
        public required string DrawOdd { get; set; }
        public required string AwayWinOdd { get; set; }
    }
}
