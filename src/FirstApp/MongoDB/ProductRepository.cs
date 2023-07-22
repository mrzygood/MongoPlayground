using FirstApp.Products;
using FirstApp.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FirstApp.MongoDB;

public sealed class ProductRepository
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductRepository(IMongoClientAccessor mongoClientAccessor, IOptions<MongoDbSettings> mongoSettings)
    {
        _productsCollection = mongoClientAccessor
            .GetClient()
            .GetDatabase(mongoSettings.Value.Database)
            .GetCollection<Product>(mongoSettings.Value.ProductsCollectionName);
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
