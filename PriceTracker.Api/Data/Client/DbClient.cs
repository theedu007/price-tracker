using Data.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Data.Client
{
    public class DbClient : MongoClient, IDbClient
    {
        private readonly PriceTrackerSettings _settings;
        public DbClient(PriceTrackerSettings settings) :base(settings.ConnectionString)
        {
            _settings = settings;
        }

        public IMongoDatabase PriceTrackerContext => GetDatabase(_settings.Database);

        public static void RegisterClassMap()
        {
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.MapIdProperty(x => x.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapProperty(x => x.VendorId).SetElementName("vendorId");
                cm.MapProperty(x => x.Name).SetElementName("name");
                cm.MapProperty(x => x.ProductId).SetElementName("productId");
                cm.MapProperty(x => x.Category).SetElementName("category");
                cm.MapProperty(x => x.Url).SetElementName("url");
            });

            BsonClassMap.RegisterClassMap<ProductPrice>(cm => 
            {
                cm.MapIdProperty(x => x.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapProperty(x => x.Price)
                    .SetSerializer(new DecimalSerializer(BsonType.String))
                    .SetElementName("price");
                cm.MapProperty(x => x.Date)
                    .SetSerializer(new DateTimeSerializer(BsonType.DateTime))
                    .SetElementName("date");
                cm.MapProperty(x => x.ProductId)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetElementName("productId");
            });
        }
    }
}
