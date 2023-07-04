using _19T1021203.BusinessLayers;
using _19T1021203.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021203.Web.Models;

namespace _19T1021203.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";
        // GET: Customer
        //public ActionResult Index(int page = 1, int pageSize = 20, String searchValue = "")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);

        //    int pageCount = rowCount / pageSize;
        //    if (rowCount % pageSize > 0)
        //    {
        //        pageCount += 1;
        //    }
        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.SearchValue = searchValue;

        //    return View(model);
        //}
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;
            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }
        public ActionResult Search(PaginationSearchInput condition)
        {

            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page,
                                                          condition.PageSize,
                                                          condition.SearchValue,
                                                          out rowCount);
            var result = new CustomerSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data

            };
            Session[CUSTOMER_SEARCH] = condition;// lưu kq tìm kiếm

            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0

            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View("Edit", data);
        }
        ///<summary>
        ///Giao diên để bổ sung
        ///</summary>
        ///<returns></returns>


        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            //int customerId = Convert.ToInt32(id);
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật khách hàng";
            return View(data);
        }
        public ActionResult Delete(String id)
        {
            int customerID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomer(customerID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            { /// kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Quốc gia không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Tên thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomer(data);
                }
                else
                {
                    CommonDataService.UpdateCustomer    (data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra vui lòng thử lại sau !");
            }
        }
    }
}