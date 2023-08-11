using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test2.Model
{
    public class CustomerReward
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CustomerId")]
        public int CustomerId { get; set; }

        [BsonElement("RewardPoints")]
        public int RewardPoints { get; set; }
    }
}
