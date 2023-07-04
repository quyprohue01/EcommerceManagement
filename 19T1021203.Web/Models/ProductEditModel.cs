using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021203.DomainModels;

namespace _19T1021203.Web.Models
{
    public class ProductEditModel
    {
        public Product Data { get; set; }
        public List<ProductAttribute> Attributes;
    }
}