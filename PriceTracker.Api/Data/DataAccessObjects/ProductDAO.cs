using Data.Client;
using Data.Collections;
using Data.DataAccessObjects.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.DataAccessObjects
{
    public class ProductDAO : IProductDAO
    {
        private readonly IDbClient _client;

        public ProductDAO(IDbClient client)
        {
            _client = client;
        }

        private IMongoCollection<Product> ProductCollection => _client.PriceTrackerContext
            .GetCollection<Product>("products");

        public Product GetProductById(string id)
        {
            var objId = new ObjectId(id);
            var filter = Builders<Product>.Filter.Eq("_id", objId);
            return ProductCollection.Find(filter).FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return ProductCollection.Find(FilterDefinition<Product>.Empty).ToList();
        }
    }
}
