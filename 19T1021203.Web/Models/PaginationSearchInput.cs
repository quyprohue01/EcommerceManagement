using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{/// <summary>
/// biểu diễn dự liệu đầu vào để tìm kiếm, phân trang chung
/// </summary>
    public class PaginationSearchInput
    {/// <summary>
     /// trang cần hiển thị
     /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// số dòng cần hiển thị mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị cần tìm
        /// </summary>
        public String SearchValue { get; set; }
    }
}