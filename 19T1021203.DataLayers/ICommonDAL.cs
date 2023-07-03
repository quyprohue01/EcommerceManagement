using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021203.DataLayers
{/// <summary>
/// Định nghĩa các phép dữ liệu chung cho các dữ liệu
/// Đơn giản dành cho các bảng
/// </summary>
    public interface ICommonDAL<T> where T : class
    {/// <summary>
     /// Tìm kiếm và lấy danh sách dưới dạng phân trang
     /// </summary>
     /// <param name="page">Trang cần hiển thị</param>
     /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
     /// <param name="searchValue">Giá trị tìm kiếm theo tên (chuỗi rỗng nếu không tìm kiếm)</param>
     /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm theo tên (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Lấy thông tin của dữ liệu dựa vào mã dữ liệu đó
        /// </summary>
        /// <param name="id">Mã của dữ liệu cần lấy thông tin</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Bổ sung thêm nhà cung cấp vào csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của dữ liệu được tạo mới</returns>
        /// 
        int Add(T data);
        /// <summary>
        /// Cập nhật thông tin 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xóa dựa vào mã 
        /// </summary>
        /// <param name="id">Mã cần xóa</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Kiểm tra hiện có dữ liệu liên quan hay k?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
