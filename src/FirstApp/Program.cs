using FirstApp.MongoDB;
using FirstApp.Products;
using FirstApp.Settings;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClientAccessor, MongoClientAccessor>();
builder.Services.AddOptions<MongoDbSettings>().Bind(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.GetRequiredService<IMongoClientAccessor>().GetClient().StartSession();

app.MapGet("/", () => "Hello World!");

app.MapPost("/products/add", async (
    ProductRepository productRepository,
    [FromBody] AddProductRequestDto productData) =>
{
    var product = new ProductDto
    {
        Id = Guid.NewGuid().ToString("N")[..24], 
        Name = productData.Name, 
        Price = productData.Price,
        Category = productData.Category,
        Description = productData.Description ?? string.Empty
    };
    var productId = await productRepository.AddAsync(product);
    
    return Results.Ok(productId);
});

app.MapGet("/products", (ProductRepository productRepository, string categoryName) 
    => productRepository.GetAsync(categoryName));

app.MapPut("/products/{id}",
    async (ProductRepository productRepository, [FromRoute] string id, [FromBody] ProductUpdateDto productUpdate) =>
    {
        var product = new Product
        {
            Id = id,
            Name = productUpdate.Name,
            Price = productUpdate.Price,
            Category = productUpdate.Category,
            Description = productUpdate.Description
        };

        await productRepository.UpdateAsync(product);
    });

app.MapDelete("/products/{id}", (ProductRepository productRepository, [FromRoute] string id) 
    => productRepository.DeleteAsync(id));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
