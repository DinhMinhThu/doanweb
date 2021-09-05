using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doan_ChuyenMon.Models;
namespace Doan_ChuyenMon.Controllers
{
    public class HomeController : Controller
    {
        DoAn_ChuyenMonEntities db = new DoAn_ChuyenMonEntities();
        public ActionResult LogOut()
        {
            Session.Remove("ten");
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _Slide()
        {
            var spmoi = db.SPs.OrderBy(s => s.NgayDang).Take(10).ToList();
            return PartialView(spmoi);
        }
        //public PartialViewResult _Product()
        //{
        //    return PartialView();
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}