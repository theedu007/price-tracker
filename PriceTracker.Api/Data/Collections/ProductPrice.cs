using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Collections
{
    public class ProductPrice
    {
        public string? Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string? ProductId { get; set; }
    }
}
