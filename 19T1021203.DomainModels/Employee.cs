using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DomainModels
{
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public String LastName { get; set; }
        /// <summary>
        /// Họ nhân viên
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public String Photo { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public String Note { get; set; }
        /// <summary>
        /// Email nhân viên
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// Mật Khẩu nhân viên
        /// </summary>
        public String Password { get; set; }
    }
}
