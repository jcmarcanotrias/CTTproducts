using CTTproducts.Models;
using CTTproducts.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CTTproducts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase, IProductController
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        // Validate the productId before fetching
        return await _productService.GetProductByIdAsync(productId);
    }

    [HttpPost]
    public async Task InsertProductAsync(Product product)
    {
        // Validate the product before inserting
        await _productService.InsertProductAsync(product);
    }
}