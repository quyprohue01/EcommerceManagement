using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021203.DomainModels;

namespace _19T1021203.DataLayers
{/// <summary>
/// định nghĩa các phép xử lý liên quan đsến dữ liệu tài khoản người dùng
/// </summary>
    public interface IUserAccountDAL
    {/// <summary>
    /// kiểm tra tên đăng nhập và mật khẩu có hợp lệ hay không ? 
    /// nếu hợp lệ thì trả về thông tin của tài khoản ngược lại trả về null
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        bool ChangePassword ( string userName , string oldPassword, string newPassword);
    }
}
