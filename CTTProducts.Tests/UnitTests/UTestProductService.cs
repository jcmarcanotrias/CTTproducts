using CTTproducts;
using CTTproducts.Models;
using CTTproducts.Repository;
using CTTproducts.Services;
using Moq;

namespace CTTProducts.Tests.UnitTests;

public class UTestProductService
{
    public UTestProductService() { }

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

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);

        IProductService productService = new ProductService(mockRepository.Object);

        // Act  
        var result = await productService.GetProductByIdAsync(productId);

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(expectedProduct, result);
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsNull()
    {
        // Arrange  
        var productId = Guid.NewGuid();
        Product? expectedProduct = null;
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);
        IProductService productService = new ProductService(mockRepository.Object);

        // Act  
        var result = await productService.GetProductByIdAsync(productId);

        // Assert  
        Assert.Null(result);
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

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(repo => repo.InsertProductAsync(product)).Returns(Task.CompletedTask);

        IProductService mockProductService = new ProductService(mockRepository.Object);

        // Act  
        await mockProductService.InsertProductAsync(product);

        // Assert  
        mockRepository.Verify(repo => repo.InsertProductAsync(It.Is<Product>(p => p.Id == product.Id)), Times.Once);
    }
}