using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021203.DomainModels;
namespace _19T1021203.DataLayers
{
    /// <summary>
    /// Các chức năng xử lý dữ liệu cho quốc gia
    /// </summary>
    /// 
    public interface ICountryDAL
    {
        /// <summary>
        /// lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> list();
    }
}
