using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Data.Collections
{
    public class ProductPrice
    {
        public string? Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }           
        public DateTime Date { get; set; }
    }
}
