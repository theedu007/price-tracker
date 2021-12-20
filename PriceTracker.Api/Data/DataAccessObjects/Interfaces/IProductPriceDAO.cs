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
        ProductPrice GetPriceForProduct(string productId);
    }
}
