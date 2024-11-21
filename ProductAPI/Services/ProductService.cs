using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Entities;
using ProductAPI.Services;

public class ProductService(AppDbContext dbContext) : IProductService
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.ModifiedAt = DateTime.UtcNow;

        dbContext.Products?.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product  = await dbContext.Products.FindAsync(id);
        if (product == null) return false;

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
        => await dbContext.Products.Include(p => p.ProductDetail).ToListAsync(); 

    public async Task<Product> GetProductByIdAsync(Guid id)
        => await dbContext.Products.Include(p => p.ProductDetail).FirstOrDefaultAsync();

    public async Task<Product> UpdateProductAsync(Guid id, Product product)
    {
        var existingProduct = await dbContext.Products.FindAsync(id);
        if(existingProduct == null) return null;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.ModifiedAt = DateTime.UtcNow;
        existingProduct.Status = product.Status;

        await dbContext.SaveChangesAsync();
        return existingProduct;


    }
}