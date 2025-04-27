using CTTproducts.Models;
using MongoDB.Driver;

namespace CTTproducts.Repository
{
    public class ProductsRepository : IProductRepository
    {        
        private const string PRODUCTS_COLLECTION_NAME = "Products";

        private IMongoDatabase _database;
        private IMongoCollection<Product> _products;

        public ProductsRepository(MongoClient mongoClient, string dataBaseName)
        {
            _database = mongoClient.GetDatabase(dataBaseName);
            _products = _database.GetCollection<Product>(PRODUCTS_COLLECTION_NAME);
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _products.Find(x => x.Id == productId).FirstOrDefaultAsync();
        }
    }
}