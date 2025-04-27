using CTTproducts.Models;
using CTTproducts.Repository;
using Mongo2Go;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests;

public class ProductRepositoryFixture : IDisposable
{

    private MongoDbRunner _mongoDbRunner;
    private const string DATABASE_NAME = "CTTProductsTest";
    private const string PRODUCTS_COLLECTION_NAME = "Products";

    public IProductRepository Repository { get; }
    public IMongoDatabase Database { get; }

    public ProductRepositoryFixture() 
    {
        _mongoDbRunner = MongoDbRunner.Start();

        var mongoclient = new MongoClient(_mongoDbRunner.ConnectionString);
        
        Database = mongoclient.GetDatabase(DATABASE_NAME);
        var collection = Database.GetCollection<Product>(PRODUCTS_COLLECTION_NAME);
        collection.DeleteMany(_ => true); // Clear the collection before each test

        Repository = new ProductRepository(mongoclient, DATABASE_NAME);
    }

    public void Dispose()
    {
        _mongoDbRunner.Dispose();
    }
}