using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirstApp.Products;

public sealed class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("category")]
    public string Category { get; set; }
    
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [BsonElement("description")]
    public string? Description { get; set; }
}