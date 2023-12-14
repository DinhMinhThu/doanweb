using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KLTN_FreshShop.Models;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace KLTN_FreshShop.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: admin/Default
        KLTNEntities db = new KLTNEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtTen, string txtMatKhau)
        {
            var nd = db.NguoiDungs.Where(s => s.TenDangNhap.Equals(txtTen) && s.MatKhau.Equals(txtMatKhau)).FirstOrDefault();
            if (nd != null)
            {
                Session["nd"] = nd.TenDangNhap;
                return RedirectToAction("Index");
            }
            ViewBag.ms = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangky(String txthoten, String txtemail, String txtten, String txtmatkhau, String txtanh)
        {
            var qr = db.NguoiDungs.Where(s => s.TenDangNhap == txtten).ToList();
            if (qr.Count > 0)
            {
                ViewBag.mess = "Trùng tên đăng nhập ";
            }
            else
            {
                NguoiDung nd = new NguoiDung();
                nd.Ten = txthoten;
                nd.Email = txtemail;
                nd.TenDangNhap = txtten;
                nd.MatKhau = txtmatkhau;
                nd.Anh = txtanh;
                db.NguoiDungs.Add(nd);
                db.SaveChanges();
                ViewBag.mess = "Đăng kí tài khoản thành công";
            }
            return View();
        }
        public ActionResult thaydoimatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult thaydoimatkhau(String txtten, String txtmatkhau1, String txtmatkhau2)
        {
            if (txtmatkhau1 != txtmatkhau2)
            {
                ViewBag.mess = "Mật khẩu nhập lại không khớp";
            }
            else
            {
                var qr = db.NguoiDungs.Where(s => s.TenDangNhap == txtten).ToList();
                NguoiDung nd = new NguoiDung();
                nd = qr.FirstOrDefault();
                nd.TenDangNhap = txtten;
                nd.MatKhau = txtmatkhau2;
                db.SaveChanges();
                ViewBag.mess = "Đổi mật khẩu thành công";
            }
            return View();
        }
        public ActionResult capmatkhaumoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult capmatkhaumoi(String txtten, String txtemail)
        {
            GuiEmail gm = new GuiEmail();
            NguoiDung nd = new NguoiDung();
            var qr = db.NguoiDungs.Where(s => s.TenDangNhap == txtten).ToList();
            nd = qr.FirstOrDefault();
            gm.Emailnguoigui = "phungdinhminh1999@gmail.com";
            gm.mathau = "Thuemminh@123";
            gm.Emailnguoinhan = txtemail;
            gm.noidung = "Đây là mật khẩu của bạn hãy giữ kín. Mật khẩu là: <b>" + nd.MatKhau + "</b>";
            gm.chude = "Cấp lại mật khẩu";
            gm.guithu();
            ViewBag.mess = "Hoàn thành cấp lại mật khẩu hãy vào Gmail để nhận lại mật khẩu";
            return View();
        }
    }
}