using Data.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccessObjects.Interfaces
{
    public interface IProductPriceDAO
    {
        List<ProductPrice> GetAllPricesForProduct(string productId);
        List<ProductPrice> GetPricesForProductBetweenDates(string productId, DateTime startDate, DateTime endDate);
    }
}
