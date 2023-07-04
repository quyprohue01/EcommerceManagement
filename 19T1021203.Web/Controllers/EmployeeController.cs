using _19T1021203.BusinessLayers;
using _19T1021203.DomainModels;
using _19T1021203.Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021203.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 1;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";

        // GET: Employee
        //public ActionResult Index(int page = 1, int pageSize = 20, String searchValue = "")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);

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
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfEmployees(condition.Page,
                                                          condition.PageSize,
                                                          condition.SearchValue,
                                                          out rowCount);
            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data

            };
            Session[EMPLOYEE_SEARCH] = condition;// lưu kq tìm kiếm

            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0

            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        ///<summary>
        ///Giao diên để bổ sung
        ///</summary>
        ///<returns></returns>


        public ActionResult Edit(int id=0)
        {
            if(id == 0)
                return RedirectToAction("Index");

            //int supplierId = Convert.ToInt32(id);
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật nhân viên";
            return View(data);
        }
        public ActionResult Delete(String id)
        {
            int EmployeeID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(EmployeeID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(EmployeeID);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee data, string birthday , HttpPostedFileBase uploadPhoto)
        {
            DateTime? d = Converter.DMYStringToDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", $" Ngày   {birthday} không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy!");
            else
               data.BirthDate = d.Value;


            
                /// kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", "Họ không được để trống");
                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Tên  không được để trống");
              
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.Password))
                    ModelState.AddModelError("Password", "Mật khẩu không được để trống");
                if (string.IsNullOrWhiteSpace(data.Note))
                    ModelState.AddModelError("Note", "Note không được để trống");
                if (string.IsNullOrWhiteSpace(data.Photo))
                    data.Photo = "";
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "CẬP NHẬT nhân viên";
                    return View("Edit", data);

                }
                if(uploadPhoto != null)
                {
                    string path = Server.MapPath("~/Photo");
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = fileName;
                }


                if (data.EmployeeID == 0)
                {
                    CommonDataService.AddEmployee(data);
                }
                else
                {
                    CommonDataService.UpdateEmployee(data);
                }
                return RedirectToAction("Index");
            
            
        }
    }
}