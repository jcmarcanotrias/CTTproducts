using CTTproducts.Models;
using CTTproducts.Repository;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests;

public class ProductRepositoryFixture : IDisposable
{
    private const string DATABASE_CONNECTION_STRING = "mongodb://localhost:27017";
    private const string PRODUCTS_COLLECTION_NAME = "Products";

    public IMongoDatabase Database { get; private set; }
    public IProductRepository Repository { get; private set; }

    public void Init(string databaseName)
    {
        var mongoclient = new MongoClient(DATABASE_CONNECTION_STRING);

        Database = mongoclient.GetDatabase(databaseName);
        var collection = Database.GetCollection<Product>(PRODUCTS_COLLECTION_NAME);
        collection.DeleteMany(_ => true); // Clear the collection before each test

        Repository = new ProductRepository(mongoclient, databaseName);
    }

    public void Dispose()
    {
        Database.DropCollection(PRODUCTS_COLLECTION_NAME);
    }
}