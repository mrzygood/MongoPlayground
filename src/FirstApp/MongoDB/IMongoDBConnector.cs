using FirstApp.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FirstApp.MongoDB;

public interface IMongoDbConnector
{
   IMongoDatabase Connect();
}

public class MongoDbConnector : IMongoDbConnector
{
    private readonly IOptions<MongoDbSettings> _mongoSettings;

    public MongoDbConnector(IOptions<MongoDbSettings> mongoSettings)
    {
        _mongoSettings = mongoSettings;
    }

    public IMongoDatabase Connect()
    {
        var settings = MongoClientSettings.FromConnectionString(_mongoSettings.Value.Url);
        settings.ConnectTimeout = TimeSpan.FromSeconds(4);
        settings.SocketTimeout = TimeSpan.FromSeconds(4);
        
        var client = new MongoClient(settings);
        return client.GetDatabase(_mongoSettings.Value.Database);
    }
}
