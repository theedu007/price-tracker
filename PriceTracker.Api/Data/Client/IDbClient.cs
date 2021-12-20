using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Client
{
    public interface IDbClient
    {
        public IMongoDatabase PriceTrackerContext { get; }
    }
}
