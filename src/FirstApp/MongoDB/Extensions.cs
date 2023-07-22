namespace FirstApp.MongoDB;

public static class Extensions
{
    public static IServiceProvider ConnectToMongo(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<IMongoClientAccessor>().GetClient().StartSession();
        return serviceProvider;
    }
}
