using Data.Client;
using Data.Collections;
using Data.DataAccessObjects.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccessObjects
{
    public class ProductPriceDAO : IProductPriceDAO
    {
        private readonly IDbClient _client;

        public ProductPriceDAO(IDbClient client)
        {
            _client = client;
        }

        private IMongoCollection<ProductPrice> ProductPriceCollection => _client.PriceTrackerContext
            .GetCollection<ProductPrice>("productPrices");

        public List<ProductPrice> GetAllPricesForProduct(string productId)
        {
            var prodId = new ObjectId(productId);
            var filter = Builders<ProductPrice>.Filter.Eq("productId", prodId);
            return ProductPriceCollection.Find(filter).ToList();
        }

        public List<ProductPrice> GetPricesForProductBetweenDates(string productId, DateTime startDate, DateTime endDate)
        {
            var prodId = new ObjectId(productId);
            var startDateFilter = Builders<ProductPrice>.Filter.Gte(x => x.Date, startDate);
            var endDateFilter = Builders<ProductPrice>.Filter.Lte(x => x.Date, endDate);
            var prodFilter = Builders<ProductPrice>.Filter.Eq("productId", prodId);
            var filter = Builders<ProductPrice>.Filter.And(prodFilter, startDateFilter, endDateFilter);
            return ProductPriceCollection.Find(filter).ToList();
        }
    }
}
