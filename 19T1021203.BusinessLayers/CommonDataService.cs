using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021203.DataLayers;
using _19T1021203.DomainModels;
namespace _19T1021203.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đên:nhà cung cấp,nhà cung cấp ,người giao hàng ,nhân viên,loại hàng
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICountryDAL countryDB;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Category> categoryDB;
        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
        }
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.list().ToList();
        }

        #region Các Nghiệp vụ liên quan đến nhà cung cấp

        /// <summary>
        /// Tìm kiếm,lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang.Bằng 0 thì không phân trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm .Giá trị bằng rỗng thì không tìm kiếm</param>
        /// <param name="rowCount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, String searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> List(String searchValue)
        {

            return supplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã của nhà cung cấp</returns>
        public static int AddSupplier(Supplier data)
        {

            return supplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }
        /// <summary>
        ///  Xoá nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);
        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);
        }
        /// <summary>
        /// Kiểm tra xem 1 nhà cung cấp hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);
        }
        #endregion


        #region Các Nghiệp vụ liên quan đến khách hàng

        /// <summary>
        /// Tìm kiếm,lấy danh sách các khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang.Bằng 0 thì không phân trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm .Giá trị bằng rỗng thì không tìm kiếm</param>
        /// <param name="rowCount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, String searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListKh(String searchValue = "")
        {

            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã của khách hàng</returns>
        public static int AddCustomer(Customer data)
        {

            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        ///  Xoá khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int CustomerID)
        {
            return customerDB.Delete(CustomerID);
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int CustomerID)
        {
            return customerDB.Get(CustomerID);
        }
        /// <summary>
        /// Kiểm tra xem 1 khách hàng hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }
        #endregion




        #region Các Nghiệp vụ liên quan đến nhân viên

        /// <summary>
        /// Tìm kiếm,lấy danh sách các nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang.Bằng 0 thì không phân trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm .Giá trị bằng rỗng thì không tìm kiếm</param>
        /// <param name="rowCount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, String searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListNv(String searchValue = "")
        {

            return employeeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã của nhân viên</returns>
        public static int AddEmployee(Employee data)
        {

            return employeeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhân viên 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }
        /// <summary>
        ///  Xoá nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int EmployeeID)
        {
            return employeeDB.Delete(EmployeeID);
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int EmployeeID)
        {
            return employeeDB.Get(EmployeeID);
        }
        /// <summary>
        /// Kiểm tra xem 1 nhân viên hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int EmployeeID)
        {
            return employeeDB.InUsed(EmployeeID);
        }
        #endregion



        #region Các Nghiệp vụ liên quan đến giao hàng

        /// <summary>
        /// Tìm kiếm,lấy danh sách các giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang.Bằng 0 thì không phân trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm .Giá trị bằng rỗng thì không tìm kiếm</param>
        /// <param name="rowCount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShipper(int page, int pageSize, String searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách giao hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListGh(String searchValue = "")
        {

            return shipperDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã của giao hàng</returns>
        public static int AddShipper(Shipper data)
        {

            return shipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }
        /// <summary>
        ///  Xoá giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int ShipperID)
        {
            return shipperDB.Delete(ShipperID);
        }
        /// <summary>
        /// Lấy thông tin của 1 giao hàng
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int ShipperID)
        {
            return shipperDB.Get(ShipperID);
        }
        /// <summary>
        /// Kiểm tra xem 1 giao hàng cấp hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int ShipperID)
        {
            return shipperDB.InUsed(ShipperID);
        }
        #endregion


        #region Các Nghiệp vụ liên quan đến Loại hàng

        /// <summary>
        /// Tìm kiếm,lấy danh sách các loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang.Bằng 0 thì không phân trang</param>
        /// <param name="searchValue">Giá trị tìm kiếm .Giá trị bằng rỗng thì không tìm kiếm</param>
        /// <param name="rowCount">Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, String searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng (không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListLh(String searchValue)
        {

            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Bổ sung loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã của loại hàng</returns>
        public static int AddCategory(Category data)
        {

            return categoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }
        /// <summary>
        ///  Xoá loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int CategoryID)
        {
            return categoryDB.Delete(CategoryID);
        }
        /// <summary>
        /// Lấy thông tin của 1 loại hàng
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int CategoryID)
        {
            return categoryDB.Get(CategoryID);
        }
        /// <summary>
        /// Kiểm tra xem 1 loại hàng cấp hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int CategoryID)
        {
            return categoryDB.InUsed(CategoryID);
        }
        #endregion

    }

}
