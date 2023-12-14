using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KLTN_FreshShop.Models;

namespace KLTN_FreshShop.Controllers
{
    public class TinTucController : Controller
    {
        KLTNEntities db = new KLTNEntities();
        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _tintuc()
        {
            ViewBag.dsTin = db.TinTucs.Where(s => s.TieuBieu == true && s.LoaiTin==null).ToList();
            return PartialView();
        }
        public PartialViewResult chomebau()
        {
            ViewBag.dsTinchomebau = db.TinTucs.Where(s => s.LoaiTin == "chomebau").ToList();
            return PartialView();
        }
        public PartialViewResult tintuchangngay()
        {
            ViewBag.dsTinTucHangNgay = db.TinTucs.Where(s => s.LoaiTin == "TinTucHangNgay").ToList();
            return PartialView();
        }
        public ActionResult timkiem(String txtsearch)
        {
            var qr = db.TinTucs.Where(s => s.MotaNgan.Contains(txtsearch)).ToList();
            ViewBag.kq = qr.Count();
            return View(qr);
        }
        public ActionResult Chitiettin(int ma)
        {
            var tint = db.TinTucs.Find(ma);
            ViewBag.tint = db.TinTucs.Where(s => s.LoaiTin == tint.LoaiTin).Take(4).ToList();
            return View(tint);
        }
    }
}