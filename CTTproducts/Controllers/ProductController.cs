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
    public async Task<ActionResult<Product?>> GetProductByIdAsync(string productId)
    {
        if(!Guid.TryParse(productId, out Guid productIdGuid))
        {
            return BadRequest("Product ID not valid.");
        }

        var product = await _productService.GetProductByIdAsync(productIdGuid);
        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> InsertProductAsync(ProductPost productPost)
    {        
        if (productPost == null)
        {
            return BadRequest("Product cannot be null.");
        }
        // Add more validation as needed
        Product product = new Product
        {
            Description = productPost.Description,
            Price = productPost.Price,
            Categories = productPost.Categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }).ToList().ToArray()
        };
        await _productService.InsertProductAsync(product);

        return Ok(product.Id);
    }
}