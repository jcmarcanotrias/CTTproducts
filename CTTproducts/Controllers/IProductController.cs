
using CTTproducts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CTTproducts.Controllers;

public interface IProductController
{
    Task<ActionResult<Product?>> GetProductByIdAsync(Guid productId);

    Task<ActionResult> InsertProductAsync(Product product);
}