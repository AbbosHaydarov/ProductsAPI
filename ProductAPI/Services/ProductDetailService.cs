using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Entities;

namespace ProductAPI.Services;

public class ProductDetailService(AppDbContext dbContext) : IProductDetailService
{
    public async Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail productDetail)
    {
        productDetail.Id = Guid.NewGuid();
        productDetail.ProductId = productId;

        dbContext.ProductDetails.Add(productDetail);
        await dbContext.SaveChangesAsync();

        return productDetail;
    }

    public async Task<bool> DeleteProductDetailAsync(Guid productId)
    {
        var detail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
        if (detail == null) return false;

        dbContext.ProductDetails.Remove(detail);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ProductDetail> GetDetailsByProductIdAsync(Guid productId)
        => await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);

    public async Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail productDetail)
    {
        var existingDetail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
        if (existingDetail == null) return null;

        existingDetail.Description = productDetail.Description;
        existingDetail.Color = productDetail.Color;
        existingDetail.Material = productDetail.Material;
        existingDetail.Weight = productDetail.Weight;
        existingDetail.QuantityInStock = productDetail.QuantityInStock;
        existingDetail.ManufactureDate = productDetail.ManufactureDate;
        existingDetail.ExpiryDate = productDetail.ExpiryDate;
        existingDetail.Size = productDetail.Size;
        existingDetail.Manufacturer = productDetail.Manufacturer;
        existingDetail.CountryOfOrigin = productDetail.CountryOfOrigin;

        await dbContext.SaveChangesAsync();
        return existingDetail;
    }
}