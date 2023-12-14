using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KLTN_FreshShop.Models;
namespace KLTN_FreshShop.Controllers
{
    public class TaiKhoanController : Controller
    {
        KLTNEntities db = new KLTNEntities();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangky(String txtten, String txtdc, String txtdt, String txtemail, String txtpassword)
        {
            var qr = db.KhachHangs.Where(s => s.Ten == txtten).ToList();
            if (qr.Count > 0)
            {
                ViewBag.ms = "Trùng tên đăng nhập! Xin nhập lại";
            }
            else
            {
                KhachHang kh = new KhachHang();
                kh.Ten = txtten;
                kh.DiaChi = txtdc;
                kh.DienThoai = txtdt;
                kh.Email = txtemail;
                kh.Password = txtpassword;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                ViewBag.ms = "Đăng ký tài khoản thành công";
            }
            return View();
        }
        public ActionResult dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangnhap(String txtemail, String txtpassword)
        {
            var qr = db.KhachHangs.Where(s => s.Email.Equals(txtemail) && s.Password.Equals(txtpassword)).ToList();
            if (qr.Count() > 0 )
            {
                Session["ten"] = qr.FirstOrDefault().Ten;
                return RedirectToAction("Index","Home");
            }
            ViewBag.ms1 = "Tên tài khoản hoặc mật khẩu không chính xác! Xin nhập lại";
            return View();
        }
        public ActionResult quenmatkhau()
        {
            return View();
        }
        [HttpPost]
        public ActionResult quenmatkhau(String txtemail)
        {
            GuiEmail gm = new GuiEmail();
            KhachHang kh = new KhachHang();
            var qr = db.KhachHangs.Where(s => s.Email == txtemail);
            kh = qr.FirstOrDefault();
            gm.Emailnguoigui = "hoahuongduong05124@gmail.com";
            gm.mathau = "dothanhnhan";
            gm.Emailnguoinhan = txtemail;
            gm.noidung = "Đây là mật khẩu của bạn hãy giữ kín. Mật khẩu là: <b>" + kh.Password + "</b>";
            gm.chude = "Cấp lại mật khẩu";
            gm.guithu();
            ViewBag.mess = "Hoàn thành cấp lại mật khẩu hãy vào Gmail để nhận lại mật khẩu";
            return View();
        }
    }
}