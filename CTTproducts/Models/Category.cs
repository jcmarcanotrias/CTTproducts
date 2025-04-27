using MongoDB.Bson.Serialization.Attributes;

namespace CTTproducts.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; }
    }
}