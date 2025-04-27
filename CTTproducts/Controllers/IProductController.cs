
using CTTproducts.Models;

namespace CTTproducts.Controllers;

public interface IProductController
{
    Task<Product> GetProductByIdAsync(Guid productId);

    Task InsertProductAsync(Product product);
}