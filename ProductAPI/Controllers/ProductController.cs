using System.Text;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Entities;
using ProductAPI.Services;

namespace ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Barcha mahsulotlarni olish
    /// </summary>
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProducts()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            CreatedAt = p.CreatedAt,
            ModifiedAt = p.ModifiedAt,
            EProductStatus = p.Status
        }));

    }

    /// <summary>
    /// Id bo'yicha olish
    /// </summary>
     
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProductById(Guid id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return Ok(new ProductReadDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt,
            EProductStatus = product.Status
        });
    }
    
    /// <summary>
    /// Yangi product yaratish
    /// </summary>
    
    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.EProductStatus
        };

        var createdProduct = await productService.CreateProductAsync(product);

        return CreatedAtAction(nameof(GetProductById), new {id = createdProduct.Id}, new ProductReadDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            CreatedAt = createdProduct.CreatedAt,
            ModifiedAt = createdProduct.ModifiedAt,
            EProductStatus = createdProduct.Status
        });
    }

    /// <summary>
    /// Id bo'yicha productni yangilash
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductReadDto>>UpdateProduct(Guid id, ProductUpdateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Status = dto.EProductStatus
        };

        var updatedProduct = await productService.UpdateProductAsync(id, product);
        if(updatedProduct == null) return NotFound();

        return Ok(new ProductReadDto
        {
            Id = updatedProduct.Id,
            Name = updatedProduct.Name,
            Price = updatedProduct.Price,
            CreatedAt = updatedProduct.CreatedAt,
            ModifiedAt = updatedProduct.ModifiedAt,
            EProductStatus = updatedProduct.Status
        });
    }

    ///<summary>
    ///Id bo'yicha productni o'chirish
    ///</summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var deletedProduct = await productService.DeleteProductAsync(id);
        if(!deletedProduct) return NotFound();

        return NoContent();
    }
}