using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    /// <summary>
    /// Lớp cơ sở cho các lớp dùng để lưu trữ kq tìm kiếm dưới dạng phân trang
    /// </summary>
    public  abstract class PaginationSearchOutput
    {/// <summary>
    ///trang được hiển thị là trang nào
    /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// số dòng mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// giá trị đang được tìm kiếm
        /// </summary>
        public String SearchValue  { get; set; }
        /// <summary>
        /// số dòng tìm được
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// tổng số trang
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
        


    }
}