using MongoDB.Bson.Serialization.Attributes;

namespace CTTproducts.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}