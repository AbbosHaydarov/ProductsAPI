using ProductAPI.Entities;

namespace ProductAPI.Services;

public interface IProductDetailService
{
    Task<ProductDetail> GetDetailsByProductIdAsync(Guid productId);
    Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail productDetail);
    Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail productDetail);
    Task<bool> DeleteProductDetailAsync(Guid productId);
}