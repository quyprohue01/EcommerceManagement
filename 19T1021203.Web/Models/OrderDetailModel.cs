using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    public class OrderDetailModel
    {

        public Order Data { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public decimal TotalPriceOrder
        {
            get
            {
                decimal SUM = 0;
                foreach (var item in OrderDetail)
                {
                    SUM += item.TotalPrice;
                }
                return SUM;
            }
        }
    }
}