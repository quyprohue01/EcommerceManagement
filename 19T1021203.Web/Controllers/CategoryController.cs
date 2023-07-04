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
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index(int page = 1, int pageSize = 5, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var data = CommonDataService.ListOfCategories(page, pageSize, searchValue, out rowCount);

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
        /// Giao dien bo sung loại hàng moi
        /// </summary>
        /// <returns></returns>



        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfCategories(condition.Page,
                                                          condition.PageSize,
                                                          condition.SearchValue,
                                                          out rowCount);
            var result = new CategorySearhOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data

            };
            Session[CATEGORY_SEARCH] = condition;// lưu kq tìm kiếm

            return View(result);
        }
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ Sung Loại Hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            //int supplierId = Convert.ToInt32(id);
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật loại hàng";
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(Category data)
        {
            try
            {
                /// kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Mô tả không được để trống");
               

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng " : "Cập nhật loại hàng";
                    return View("Edit", data);

                }


                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategory(data);
                }
                else
                {
                    CommonDataService.UpdateCategory(data);
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
            int categoryID = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategory(categoryID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
