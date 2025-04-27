
using CTTproducts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CTTproducts.Controllers;

public interface IProductController
{
    Task<ActionResult<Product?>> GetProductByIdAsync(string productId);

    Task<ActionResult> InsertProductAsync(ProductPost productPost);
}