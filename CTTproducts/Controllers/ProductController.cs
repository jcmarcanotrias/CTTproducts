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
    public async Task<ActionResult<Product?>> GetProductByIdAsync(Guid productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if(product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> InsertProductAsync(Product product)
    {        
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }
        // Add more validation as needed

        await _productService.InsertProductAsync(product);

        return CreatedAtAction(nameof(GetProductByIdAsync), new { id = product.Id }, product);
    }
}