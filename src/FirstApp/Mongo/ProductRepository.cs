using FirstApp.Products;
using FirstApp.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FirstApp.Mongo;

public sealed class ProductRepository
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductRepository(IOptions<MongoDbSettings> mongoSettings)
    {
        var settings = MongoClientSettings.FromConnectionString(mongoSettings.Value.Url);
        settings.ConnectTimeout = TimeSpan.FromSeconds(4);
        settings.SocketTimeout = TimeSpan.FromSeconds(4);
        //
        // BsonClassMap.RegisterClassMap<Product>(classMap =>
        // {
        //     classMap.AutoMap();
        //     classMap.MapMember(p => p.Name).SetElementName("name");
        //     classMap.MapMember(p => p.Price).SetElementName("price");
        //     classMap.MapMember(p => p.Category).SetElementName("category");
        //     classMap.MapMember(p => p.Description).SetElementName("description");
        // });

        
        var client = new MongoClient(settings);
        var database = client.GetDatabase(mongoSettings.Value.Database);
        _productsCollection = database.GetCollection<Product>(mongoSettings.Value.ProductsCollectionName);
    }

    public async Task<ICollection<Product>> GetAsync(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(r => r.Category, categoryName);
        var res = await _productsCollection.Find(filter).ToListAsync();

        return res;
    }

    public async Task<string> AddAsync(ProductDto product)
    {
        await _productsCollection.InsertOneAsync(new Product
        {
           Id = product.Id,
           Name = product.Name,
           Price = product.Price,
           Category = product.Category,
           Description = product.Description
        });
        return product.Id;
    }

    public async Task UpdateAsync(Product product)
    {
        var condition = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
        await _productsCollection.ReplaceOneAsync(condition, product);
    }

    public async Task DeleteAsync(string id)
    {
        await _productsCollection.DeleteOneAsync(p => p.Id == id);
    }
}
