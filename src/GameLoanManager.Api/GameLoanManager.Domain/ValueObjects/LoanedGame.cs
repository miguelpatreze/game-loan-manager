using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GameLoanManager.Domain.ValueObjects
{
    public class LoanedGame
    {
        public LoanedGame(string id, string name, DateTime loanedAt)
        {
            GameId = id;
            Name = name;
            LoanedAt = loanedAt;
        }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string GameId { get; set; }
        public string Name { get; private set; }
        public DateTime LoanedAt { get; private set; }
    }
}
