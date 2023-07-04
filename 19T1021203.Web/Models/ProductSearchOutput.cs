using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    public class ProductSearchOutput : PaginationSearchOutput
    {
        public int CategoryID { get; set; } = 0;
        public int SupplierID { get; set; } = 0;
        public List<Product> Data { get; set; }
    }
}