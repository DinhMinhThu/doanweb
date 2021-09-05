using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Doan_ChuyenMon.Controllers
{
    public class footerController : Controller
    {
        // GET: footer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dscuahang()
        {
            return View();
        }
        public ActionResult hdmuahang()
        {
            return View();
        }
        public ActionResult csdoitra()
        {
            return View();
        }
    }
}