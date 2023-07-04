using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021203.BusinessLayers;
using _19T1021203.DomainModels;

namespace _19T1021203.Web
{/// <summary>
/// cung cấp hàm tiện ích liên quan đến SelectList
/// </summary>
    public static class SelectListHelper
    {
        public static List<SelectListItem>Country()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value="" ,
                Text="-- Chọn quốc gia --"

            });
            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName,
                }) ;
            }
            return list;
        }
        public static List<SelectListItem> ListofNameCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "---Loại Hàng----"
            });
            foreach (var item in ProductDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }

        public static List<SelectListItem> ListofNameSuppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "---Nhà cung cấp----"
            });
            foreach (var item in ProductDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }
        public static List<SelectListItem> Status()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "-99",
                Text = "--Tất cả trạng thái--"
            });
            foreach (var item in OrderService.getListOrderStatus())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Status.ToString(),
                    Text = item.Description
                });
            }
            return list;
        }
    }

}
