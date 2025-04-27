using CTTproducts.Controllers;
using CTTproducts.Models;
using CTTproducts.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTTProducts.Tests.IntegrationTests
{
    public class ITestProductController:IClassFixture<ProductRepositoryFixture>
    {
        private readonly ProductRepositoryFixture _productRepositoryFixture;
        private readonly IMongoCollection<Product> _productCollection;
        public ITestProductController(ProductRepositoryFixture productRepositoryFixture)
        {
            _productRepositoryFixture = productRepositoryFixture;
            _productCollection = _productRepositoryFixture.Database.GetCollection<Product>("Products");
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Stock = 0,
                Description = "Test Product 1",
                Categories =
                   [
                       new Category
                       {
                           Id = Guid.NewGuid(),
                           Name = "Category 1"
                       }
                   ],
                Price = 2.0f
            };
            await _productCollection.InsertOneAsync(product);
            var _productService = new ProductService(_productRepositoryFixture.Repository);
            var productController = new ProductController(_productService);

            // Act
            var result = await productController.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task InsertProductAsync_InsertProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Stock = 0,
                Description = "Test Product 2",
                Categories =
                   [
                       new Category
                       {
                           Id = Guid.NewGuid(),
                           Name = "Category 2"
                       }
                   ],
                Price = 3.0f
            };

            // Act            
            var _productService = new ProductService(_productRepositoryFixture.Repository);
            var productController = new ProductController(_productService);

            await productController.InsertProductAsync(product);

            // Assert
            var insertedProduct = await _productCollection.Find(p => p.Id == productId).FirstOrDefaultAsync();
            Assert.NotNull(insertedProduct);
            Assert.Equal(product.Description, insertedProduct.Description);
        }
    }
}
