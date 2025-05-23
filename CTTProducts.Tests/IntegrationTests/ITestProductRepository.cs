﻿using CTTproducts.Models;
using CTTproducts.Repository;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests;

public class ITestProductRepository:IClassFixture<ProductRepositoryFixture>
{
    private readonly ProductRepositoryFixture _productRepositoryFixture;
    private readonly IProductRepository _repository;
    private readonly IMongoCollection<Product> _productCollection;

    public ITestProductRepository(ProductRepositoryFixture productRepositoryFixture) 
    {
        _productRepositoryFixture = productRepositoryFixture;
        _productRepositoryFixture.Init("TestRepo");
        _repository = _productRepositoryFixture.Repository;
        _productCollection = _productRepositoryFixture.Database.GetCollection<Product>("Products");
    }

    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct()
    {
        // Arrange  
        var product = new Product
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
                    
        await _productCollection.InsertOneAsync(product);

        // Act  
        var result = await _repository.GetProductByIdAsync(Guid.Parse(product.Id));

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
            Price = 2.0f
        };

        // Act  
        await _repository.InsertProductAsync(product);
        var result = await _productCollection.Find(x => x.Id == product.Id).FirstOrDefaultAsync();

        // Assert  
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }
}
