using FirstApp.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FirstApp.MongoDB;

public interface IMongoClientAccessor
{ 
    IMongoClient GetClient();
}

public class MongoClientAccessor : IMongoClientAccessor
{
    private readonly IOptions<MongoDbSettings> _mongoSettings;
    private IMongoClient? _mongoClient;

    public MongoClientAccessor(IOptions<MongoDbSettings> mongoSettings)
    {
        _mongoSettings = mongoSettings;
    }

    public IMongoClient GetClient()
    {
        if (_mongoClient != null)
        {
            return _mongoClient;
        }
        
        var settings = MongoClientSettings.FromConnectionString(_mongoSettings.Value.Url);
        settings.ConnectTimeout = TimeSpan.FromSeconds(4);
        settings.SocketTimeout = TimeSpan.FromSeconds(4);
        
        _mongoClient = new MongoClient(settings);

        return _mongoClient;
    }
}
