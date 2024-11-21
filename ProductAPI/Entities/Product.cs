using ProductAPI.Shared;

namespace ProductAPI.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public EProductStatus Status { get; set; }

    // faqat navigatsiya uchun ishlatiladi
    public ProductDetail? ProductDetail { get; set; }
}
