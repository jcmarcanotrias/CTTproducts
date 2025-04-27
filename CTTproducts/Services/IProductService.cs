
using CTTproducts.Models;

namespace CTTproducts.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid productId);
        Task InsertProductAsync(Product product);
    }
}