using Data.Collections;
using Data.DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Resolver
{
    public class ProductPriceResolver
    {
        private readonly IProductPriceDAO _dataAccess;

        public ProductPriceResolver(IProductPriceDAO dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public ProductPrice GetPriceForProduct(string productId) => _dataAccess.GetPriceForProduct(productId);
    }
}
