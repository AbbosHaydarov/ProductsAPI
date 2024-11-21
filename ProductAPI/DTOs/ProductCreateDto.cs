using ProductAPI.Shared;

namespace ProductAPI.DTOs;

public class ProductCreateDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public EProductStatus EProductStatus { get; set; }
}
