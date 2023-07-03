using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DomainModels
{
    public class Customer
    {
        /// <summary>
        /// Mã khách hàng
        /// </summary>

        public int CustomerID { get; set; }
        /// <summary>
        /// tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// tên liên hệ
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// mã bưu điện
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// mật khẩu
        /// </summary>
        public string Password { get; set; }

    }
}
