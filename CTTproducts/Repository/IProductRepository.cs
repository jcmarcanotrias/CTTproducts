using CTTproducts.Models;

namespace CTTproducts.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(Guid productId);
    }
}