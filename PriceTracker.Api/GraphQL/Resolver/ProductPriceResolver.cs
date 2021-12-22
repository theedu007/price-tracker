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

        public List<ProductPrice> GetAllPricesForProduct(string productId) => _dataAccess.GetAllPricesForProduct(productId);

        public List<ProductPrice> GetPricesForProductBetweenDates(string productId, DateTime startDate, DateTime endDate) =>
            _dataAccess.GetPricesForProductBetweenDates(productId, startDate, endDate);
    }
}
