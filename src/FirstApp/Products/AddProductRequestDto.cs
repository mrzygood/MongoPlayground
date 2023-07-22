namespace FirstApp.Products;

public sealed record AddProductRequestDto(
    string Name,
    string Category,
    decimal Price,
    string? Description = null);
