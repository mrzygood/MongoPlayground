namespace FirstApp.Settings;

public sealed class MongoDbSettings
{
    public string Url { get; set; }
    public string Database { get; set; }
    public string ProductsCollectionName { get; set; }
}