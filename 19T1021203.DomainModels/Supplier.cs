using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DomainModels
{
    /// <summary>
    /// /// <summary>
    /// Thông tin nhà cung cấp
    /// </summary>
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Mã nhà cc
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// Tên liên lạc
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string PostalCode { get; set; }
    }
}
