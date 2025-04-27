using CTTproducts.Controllers;
using CTTproducts.Models;
using CTTproducts.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CTTProducts.Tests.UnitTests
{
    public class UTestProductController
    {
        [Fact]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange  
            var productId = Guid.NewGuid();
            var expectedProduct = new Product
            {
                Id = productId,
                Stock = 0,
                Description = "Test Product 1",
                Categories = new[]
                {
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Category 1"
                            }
                        },
                Price = 2.0f
            };

            var productService = new Mock<IProductService>();
            productService.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);

            IProductController productController = new ProductController(productService.Object);

            // Act  
            var result = await productController.GetProductByIdAsync(productId);

            // Assert  
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
            Assert.Equal(expectedProduct, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsNotFound()
        {
            // Arrange  
            var productId = Guid.NewGuid();
            Product? expectedProduct = null;

            var productService = new Mock<IProductService>();
            productService.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);
            IProductController productController = new ProductController(productService.Object);

            // Act  
            var result = await productController.GetProductByIdAsync(productId);

            // Assert  
            Assert.NotNull(result);
            Assert.NotNull(result.Result);
            Assert.Equal(404, ((NotFoundResult)result.Result).StatusCode);
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
                Categories = new[]
                {
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Category 2"
                            }
                        },
                Price = 3.0f
            };

            var productService = new Mock<IProductService>();
            productService.Setup(repo => repo.InsertProductAsync(product)).Returns(Task.CompletedTask);

            IProductController productController = new ProductController(productService.Object);

            // Act  
            await productController.InsertProductAsync(product);

            // Assert  
            productService.Verify(repo => repo.InsertProductAsync(product), Times.Once);
        }
    }
}
