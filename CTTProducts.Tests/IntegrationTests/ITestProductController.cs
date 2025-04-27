using CTTproducts.Controllers;
using CTTproducts.Models;
using CTTproducts.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests
{
    public class ITestProductController : IClassFixture<ProductRepositoryFixture>
    {
        private readonly ProductRepositoryFixture _productRepositoryFixture;
        private readonly IMongoCollection<Product> _productCollection;
        public ITestProductController(ProductRepositoryFixture productRepositoryFixture)
        {
            _productRepositoryFixture = productRepositoryFixture;
            _productRepositoryFixture.Init("TestControllers");
            _productCollection = _productRepositoryFixture.Database.GetCollection<Product>("Products");
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var expectedProduct = new Product
            {
                Id = Guid.NewGuid().ToString(),
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
            await _productCollection.InsertOneAsync(expectedProduct);
            var _productService = new ProductService(_productRepositoryFixture.Repository);
            var productController = new ProductController(_productService);

            // Act
            var result = await productController.GetProductByIdAsync(expectedProduct.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
            var resultProduct = ((OkObjectResult)result.Result).Value as Product;
            Assert.NotNull(resultProduct);
            Assert.Equal(expectedProduct.Id, resultProduct.Id);
        }

        [Fact]
        public async Task InsertProductAsync_InsertProduct()
        {
            // Arrange
            var product = new ProductPost
            {                
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

            // Act            
            var _productService = new ProductService(_productRepositoryFixture.Repository);
            var productController = new ProductController(_productService);

            var result = await productController.InsertProductAsync(product);

            // Assert
            Assert.NotNull(result);
            //var insertedProduct = await _productCollection.Find(p => p.Id == product.Id).FirstOrDefaultAsync();
            //Assert.NotNull(insertedProduct);
            //Assert.Equal(product.Id, insertedProduct.Id);
        }
    }
}
