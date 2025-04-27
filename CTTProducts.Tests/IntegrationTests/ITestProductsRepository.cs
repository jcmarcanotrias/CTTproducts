using CTTproducts.Models;
using CTTproducts.Repository;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests;

public class ITestProductsRepository:IClassFixture<MongoDbFixture>
{
    private readonly MongoDbFixture _mongoDbFixture;
    private readonly IProductRepository _repository;
    private readonly IMongoCollection<Product> _productCollection;

    public ITestProductsRepository(MongoDbFixture mongoDbFixture) 
    {
        _mongoDbFixture = mongoDbFixture;
        _repository = _mongoDbFixture.Repository;
        _productCollection = _mongoDbFixture.Database.GetCollection<Product>("Products");
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

        // Act  
        var result = await _repository.GetProductByIdAsync(productId);

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
            Price = 2.0f
        };

        // Act  
        await _repository.InsertProductAsync(product);
        var result = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }
}
