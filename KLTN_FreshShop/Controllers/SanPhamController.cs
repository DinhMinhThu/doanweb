using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KLTN_FreshShop.Models;
namespace KLTN_FreshShop.Controllers
{
    public class SanPhamController : Controller
    {
        KLTNEntities db = new KLTNEntities();
        // GET: SanPham
        public ActionResult Chitiet(int Ma)
        {
            var sanp = db.SPs.Find(Ma);
            ViewBag.spcungloai = db.SPs.Where(s => s.MaLoai == sanp.MaLoai).Take(4).ToList();
            return View(sanp);
        }
        public PartialViewResult _Product()
        {
            int maloai4 = 4;
            int maloai5 = 5;
            int maloai6 = 6;
            int maloai7 = 7;
            int maloai9 = 9;
            int maloai10 = 10;
            var dssp4 = db.SPs.Where(s => s.MaLoai == maloai4);
            ViewBag.dssp4 = dssp4.ToList();
            var dssp5 = db.SPs.Where(s => s.MaLoai == maloai5);
            ViewBag.dssp5 = dssp5.ToList();
            var dssp6 = db.SPs.Where(s => s.MaLoai == maloai6);
            ViewBag.dssp6 = dssp6.ToList();
            var dssp7 = db.SPs.Where(s => s.MaLoai == maloai7);
            ViewBag.dssp7 = dssp7.ToList();
            var dssp9 = db.SPs.Where(s => s.MaLoai == maloai9);
            ViewBag.dssp9 = dssp9.ToList();
            var dssp10 = db.SPs.Where(s => s.MaLoai == maloai10);
            ViewBag.dssp10 = dssp10.ToList();
            return PartialView();
        }
        public ActionResult store_grid(int ma)
        {
            var sptheoloai = db.SPs.Find(ma);
            ViewBag.sptl = db.SPs.Where(s => s.MaLoai == ma).ToList();
            var qr = from s in db.LoaiSPs where s.Ma == ma select s.Ten;
            ViewBag.tenloaisp = qr.FirstOrDefault();
            int gia = 100000;
            var qr100 = db.SPs.Where(s => s.Gia < gia && s.MaLoai==ma).FirstOrDefault();
            ViewBag.gia100 = qr100.Ma;
            var qr1 = from s in db.SPs where s.MaLoai==ma select s.Ma;
            ViewBag.dem = qr1.Count();
            return View(sptheoloai);
        }
      
        public ActionResult timkiem(String txtsearch)
        {
            var qr = db.SPs.Where(s => s.Ten.Contains(txtsearch)).ToList();
            ViewBag.kq = qr.Count();
            return View(qr);
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}