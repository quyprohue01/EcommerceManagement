using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021203.Web.Controllers
{
    public class testController : Controller
    {
        [HttpGet]
        public ActionResult Input()
        {
            Person p = new Person()
            {
                BirthDate = new DateTime(2001, 22, 22),
            };

            return View(p);// truyen p vao view
        }

        [HttpPost]
        public ActionResult Input(Person p)
        {
            var data = new
            {
                Name = p.Name,
                BirthDate = string.Format("{0:dd/MM/yyyy}", p.BirthDate),
                Salary = p.Salary
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[Route("Xin-chao/{name}")]
        //public string SayHello(string name)
        //{
        //    return $"Heyy{name}";
        //}
        public string TestDate (DateTime value)
        {
            DateTime d = value;
            return string.Format("{0: dd/MM/yyyy}", d) ;
        }

    }


    public class Person
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public float Salary { get; set; }
    }

}