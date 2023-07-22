﻿namespace FirstApp.Products;

public sealed class ProductUpdateDto
{
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
