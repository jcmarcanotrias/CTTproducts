using CTTproducts.Models;
using CTTproducts.Services;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests
{
    public class ITestProductService : IClassFixture<ProductRepositoryFixture>
    {
        private readonly ProductRepositoryFixture _productRepositoryFixture;
        private readonly IMongoCollection<Product> _productCollection;

        public ITestProductService(ProductRepositoryFixture productRepositoryFixture)
        {
            _productRepositoryFixture = productRepositoryFixture;
            _productRepositoryFixture.Init("TestServices");
            _productCollection = _productRepositoryFixture.Database.GetCollection<Product>("Products");
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId.ToString(),
                Stock = 0,
                Description = "Test Product 1",
                Categories =
                   [
                       new Category
                       {
                           Id = Guid.NewGuid().ToString(),
                           Name = "Category 1"
                       }
                   ],
                Price = 2.0f
            };

            await _productCollection.InsertOneAsync(product);
            var productService = new ProductService(_productRepositoryFixture.Repository);

            // Act
            var result = await productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task InsertProductAsync_InsertProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = "",
                Stock = 0,
                Description = "Test Product 2",
                Categories =
                   [
                       new Category
                       {
                           Id = Guid.NewGuid().ToString(),
                           Name = "Category 2"
                       }
                   ],
                Price = 3.0f
            };
            var productService = new ProductService(_productRepositoryFixture.Repository);

            // Act
            await productService.InsertProductAsync(product);

            // Assert
            var insertedProduct = await _productCollection.Find(p => p.Id == product.Id).FirstOrDefaultAsync();
            Assert.NotNull(insertedProduct);
            Assert.Equal(product.Id, insertedProduct.Id);
        }
    }
}
