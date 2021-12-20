using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Collections
{
    public class Product
    {
        public string? Id { get; set; }
        public string? VendorId { get; set; }
        public string? Name { get; set; }
        public string? ProductId { get; set; }
        public string? Category { get; set; }
        public string? Url { get; set; }
    }
}
