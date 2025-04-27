using CTTproducts.Models;
using CTTproducts.Repository;

namespace CTTproducts.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
            }
            // Other validations can be added here as needed

            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task InsertProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            // Other validations can be added here as needed

            await _productRepository.InsertProductAsync(product);
        }
    }
}