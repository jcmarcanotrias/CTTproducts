using CTTproducts;
using CTTproducts.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTTProducts.Tests.IntegrationTests
{
    public class ITestProductsRepository
    {
        private IMongoDatabase _database;
        private IMongoCollection<Product> _collection;
        private const string MONGODB_CONNECTION_STRING = "mongodb://localhost:27017";

        public ITestProductsRepository() 
        {
            var client = new MongoClient(MONGODB_CONNECTION_STRING);
            _database = client.GetDatabase("TestDatabase");
            _collection = _database.GetCollection<Product>("Products");
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
                Description = "Test Product",
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
            await _collection.InsertOneAsync(product);

            // Act  
            var repository = new ProductsRepository(new MongoClient(MONGODB_CONNECTION_STRING));
            var result = await repository.GetProductByIdAsync(productId);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }
    }
}
