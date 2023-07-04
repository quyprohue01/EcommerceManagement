using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _19T1021203.BusinessLayers;
using _19T1021203.DomainModels;
namespace _19T1021203.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassWord()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName = "", string password = "")
        {

            ViewBag.UserName = userName;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {

                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }


            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);

            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }

            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(string userName = "", string oldPassword = "", string newPassword = "")
        {

            ViewBag.UserName = userName;

            if (string.IsNullOrWhiteSpace(newPassword))
            {

                ModelState.AddModelError("", "Vui lòng nhập mật khẩu mới");
                return View();
            }

            var changePasswordUser = UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);

            if (!changePasswordUser)
            {
                ModelState.AddModelError("", "Đổi mật khẩu thất bại");
                return View();
            }

            //string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            //FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Login");

            return View();
        }

    }

}

