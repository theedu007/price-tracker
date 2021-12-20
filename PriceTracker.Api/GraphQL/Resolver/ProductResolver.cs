using Data.Client;
using Data.Collections;
using Data.DataAccessObjects.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Resolver
{
    public class ProductResolver
    {
        private readonly IProductDAO _dataAccess;
        public ProductResolver(IProductDAO dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Product> GetProducts() => _dataAccess.GetProducts();

        public Product GetProduct(string id) => _dataAccess.GetProductById(id);
    }
}
