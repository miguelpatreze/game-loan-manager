using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GameLoanManager.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
            Enabled = true;
            CreatedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
