using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021203.BusinessLayers;
using _19T1021203.DomainModels;
using _19T1021203.Web.Models;

namespace _19T1021203.Web.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SHIPPER_SEARCH = "SearchShipperCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfShipper(page, pageSize, searchValue, out rowCount);

        //    int pageCount = rowCount / pageSize;
        //    if (rowCount % pageSize > 0)
        //        pageCount += 1;

        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.SearchValue = searchValue;

        //    return View(data);
        //    //trả về giao diện mà có truyền thêm dữ liệu cho giao diện (data)
        //}
        /// <summary>
        /// Giao dien bo sung nguoi giao hang moi
        /// </summary>
        /// <returns></returns>
        /// 


        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfShipper(condition.Page,
                                                          condition.PageSize,
                                                          condition.SearchValue,
                                                          out rowCount);
            var result = new ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data

            };
            Session[SHIPPER_SEARCH] = condition;// lưu kq tìm kiếm

            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ Sung Người Giao Hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// giao diện để bổ sung
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            //int supplierId = Convert.ToInt32(id);
            var data = CommonDataService.GetShipper(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật người giao hàng";
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(Shipper data)
        {
            try
            {
                /// kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên người giao hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung người giao hàng " : "CẬP NHẬT người giao hàng";
                    return View("Edit", data);

                }


                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShipper(data);
                }
                else
                {
                    CommonDataService.UpdateShipper(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //ghi lại log lỗi
                return Content("Có lỗi xảy ra vui lòng thử lại sau !");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int shipperID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetShipper(shipperID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}


