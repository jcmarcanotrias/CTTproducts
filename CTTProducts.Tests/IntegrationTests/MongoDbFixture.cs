using CTTproducts.Models;
using CTTproducts.Repository;
using MongoDB.Driver;

namespace CTTProducts.Tests.IntegrationTests
{
    public class MongoDbFixture : IDisposable
    {

        private const string DATABASE_CONNECTION_STRING = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "CTTProductsTest";
        private const string PRODUCTS_COLLECTION_NAME = "Products";

        public IProductRepository Repository { get; }
        public IMongoDatabase Database { get; }

        public MongoDbFixture() 
        {
            var mongoclient = new MongoClient(DATABASE_CONNECTION_STRING);
            
            Database = mongoclient.GetDatabase(DATABASE_NAME);
            var collection = Database.GetCollection<Product>(PRODUCTS_COLLECTION_NAME);
            collection.DeleteMany(_ => true); // Clear the collection before each test

            Repository = new ProductsRepository(mongoclient, DATABASE_NAME);
        }

        public void Dispose()
        {
            Database.DropCollection(PRODUCTS_COLLECTION_NAME);
        }
    }
}