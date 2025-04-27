using MongoDB.Bson.Serialization.Attributes;

namespace CTTproducts.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        public int Stock { get; set; } = 0;

        public string Description { get; set; }

        public Category[] Categories { get; set; }

        public float Price { get; set; }
    }
}