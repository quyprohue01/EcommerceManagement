using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm khách hàng dưới dạng phân trang
    /// </summary>
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}