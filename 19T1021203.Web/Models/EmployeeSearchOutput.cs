using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021203.Web.Models
{
    /// <summary>
    /// kết quả tìm kiếm nhân viên dưới dạng phân trang
    /// </summary>
    public class EmployeeSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách nhà cung cấp
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}