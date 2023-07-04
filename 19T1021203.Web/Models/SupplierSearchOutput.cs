using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm nhà cung cấp dưới dạng phân trang
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách nhà cung cấp
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}