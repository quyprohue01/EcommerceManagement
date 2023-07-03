using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021203.DomainModels;


namespace _19T1021203.DataLayers
{/// <summary>
 /// 
 /// Định nghĩa các phép xử lý dữ liệu trên nhà cung cấp
 /// </summary>
    public interface ISupplierDAL
    {/// <summary>
     /// 
     ///Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang 
     /// </summary>
     /// <param name="page"> trang cần hiển thị</param>
     /// <param name="pageSize">Số dòng được hiển thị trên mỗi trang(0 tức là không yêu cầu phân trang)</param>
     /// <param name="searchValue">Tên cần tìm kiếm (chuỗi rỗng nếu  không tìm kiếm theo tên)</param>
     /// <returns></returns>
        IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đêm số nhà cung cấp tìm được ở dòng 29
        /// </summary>
        /// <param name="searchValue"> Tên cần tìm kiếm(chuỗi rỗng nếu  không tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Bổ sung thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của nhà cung cấp được tạo mới></returns>
        int Add(Category data);
        /// <summary>
        /// Cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// Xóa một nhà cung cấp dựa vào mã của nhà cung cấp
        /// </summary>
        /// <param name="supplierID"> Mã của nhà cung cấp cần xóa</param>
        /// <returns></returns>
        bool Delete(int supplierID);
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp dựa vào mã của nhà cung cấp
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        Category Get(int supplier);
        /// <summary>
        /// Kiểm tra xem nhà cung cấp  hiện có dữ liệu liên quan hay không ?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InUsed(int supplierID);

    }
}
