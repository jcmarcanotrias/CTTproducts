using CTTproducts.Models;
using CTTproducts.Repository;
using MongoDB.Driver;

namespace CTTproducts
{
    public class ProductsRepository : IProductRepository
    {
        private MongoClient mongoClient;

        public ProductsRepository(MongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public Task<Product> GetProductByIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}