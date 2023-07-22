namespace FirstApp.Products;

public sealed class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}