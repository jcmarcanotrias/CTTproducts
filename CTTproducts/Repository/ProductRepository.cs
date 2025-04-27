using CTTproducts.Models;
using MongoDB.Driver;

namespace CTTproducts.Repository;

public class ProductRepository : IProductRepository
{        
    private const string PRODUCTS_COLLECTION_NAME = "Products";

    private IMongoDatabase _database;
    private IMongoCollection<Product> _products;

    public ProductRepository(MongoClient mongoClient, string dataBaseName)
    {
        _database = mongoClient.GetDatabase(dataBaseName);
        _products = _database.GetCollection<Product>(PRODUCTS_COLLECTION_NAME);
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        // Validations can be added here as needed
        return await _products.Find(x => x.Id == productId.ToString()).FirstOrDefaultAsync();
    }

    public async Task InsertProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        }

        product.Id = Guid.NewGuid().ToString();
        // Validations can be added here as needed

        await _products.InsertOneAsync(product);
    }
}