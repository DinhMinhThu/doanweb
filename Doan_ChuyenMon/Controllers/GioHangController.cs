using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doan_ChuyenMon.Models;
namespace Doan_ChuyenMon.Controllers
{
    public class GioHangController : Controller
    {
        DoAn_ChuyenMonEntities db = new DoAn_ChuyenMonEntities();
        // GET: GioHang
        const string dsgh = "dsgh";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Xoa(int ma)
        {
            //lấy giỏ hàng về
            List<GioHang> danhsachgh = (List<GioHang>)Session[dsgh];
            //tìm sản phẩm cho mã
            var spham = danhsachgh.Where(s => s.Spham.Ma == ma).FirstOrDefault();
            danhsachgh.Remove(spham);
            //cập nhập lại giỏ hàng
            Session[dsgh] = danhsachgh;
            //trở về
            return RedirectToAction("XemGioHang");

        }
        public ActionResult TangSL(int ma)
        {
            //lấy giỏ hàng về
            List<GioHang> danhsachgh = (List<GioHang>)Session[dsgh];
            //tìm sản phẩm cho mã
            var spham = danhsachgh.Where(s => s.Spham.Ma == ma).FirstOrDefault();
            //tăng số lượng 
            spham.SoLuong += 1;
            //cập nhập lại giỏ hàng
            Session[dsgh] = danhsachgh;
            //trở về
            return RedirectToAction("XemGioHang");
        }
        public ActionResult GiamSL(int ma)
        {
            //lấy giỏ hàng về
            List<GioHang> danhsachgh = (List<GioHang>)Session[dsgh];
            //tìm sản phẩm cho mã
            var spham = danhsachgh.Where(s => s.Spham.Ma == ma).FirstOrDefault();
            //tăng số lượng 
            if (spham.SoLuong > 1)
            {
                //giảm 
                spham.SoLuong -= 1;
                //cập nhập lại session giỏ hàng
                Session[dsgh] = danhsachgh;
            }
            //cập nhập lại giỏ hàng
            Session[dsgh] = danhsachgh;
            //trở về
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XemGioHang()
        {
            List<GioHang> danhsachgh = (List<GioHang>)Session[dsgh];
            if (danhsachgh == null)
            {
                //trả về trnag hiện hành
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                ViewBag.dsgh = danhsachgh;
            }
            return View();
        }
        //thêm 1 sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult ThemGioHang(int ma, int soluong)
        {
            if (Session["ten"] != null)
            {
                //lấy sản phẩm về
                var spham = db.SPs.Find(ma);
                //lấy giở hàng từ biến session giỏ hàng
                List<GioHang> danhsachgh = (List<GioHang>)Session[dsgh];
                //ktra giỏ hàng có rỗng ko
                if (danhsachgh == null)
                {
                    //khởi tạo 1 danh sách mới
                    danhsachgh = new List<GioHang>();
                    //khởi tạo 1 phần tử  mới trong giỏ hàng
                    GioHang gh = new GioHang(spham, soluong);
                    //thêm phần tử mới vào giỏ hàng
                    danhsachgh.Add(gh);
                }
                else
                {
                    //ktra xem san phẩm đã có trong giỏ hàng chưa
                    if (danhsachgh.Exists(s => s.Spham.Ma == ma))
                    {
                        //tìm đến sản phẩm
                        var sanphammoi = danhsachgh.Where(s => s.Spham.Ma == ma).FirstOrDefault();
                        //cập nhập số lượng
                        sanphammoi.SoLuong += soluong;
                    }
                    else
                    {
                        //sản phẩm chưa có trong giỏ hàng
                        //khởi tạo 1 phần tử  mới trong giỏ hàng
                        GioHang gh = new GioHang(spham, soluong);
                        //thêm phần tử mới vào giỏ hàng
                        danhsachgh.Add(gh);
                    }
                }
                //cập nhaapk=j lại thông tin giỏ hàng
                Session[dsgh] = danhsachgh;
                //trả lại trang hiện hành
                return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("dangnhap", "TaiKhoan");
            
        }
        public ActionResult thanhtoan()
        {
            var gh = (List<GioHang>) Session[dsgh];
            return View(gh);
        }
        [HttpPost]
        public ActionResult thanhtoan(String txtten, String txtdc, String txtdt, String txtemail, String pttt)
        {
            DonHang dh = new DonHang();
            dh.TenNguoiNhan = txtten;
            dh.NgayDat = DateTime.Now;
            dh.DienThoai = txtdt;
            dh.Email = txtemail;
            dh.DiaChi = txtdc;
            dh.PhuongThucTT = pttt;
            db.DonHangs.Add(dh);
            db.SaveChanges();
            var ghs = (List<GioHang>)Session[dsgh];
            foreach (var item in ghs)
            {
                
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = dh.Ma;
                ctdh.MaSP = item.Spham.Ma;
                ctdh.SoLuong = item.SoLuong;
                if(item.Spham.KhuyenMai==null)
                {
                    ctdh.DonGia = item.Spham.Gia;
                }
                else
                {
                    decimal tt = Convert.ToDecimal(item.Spham.Gia * (100 - item.Spham.KhuyenMai) / 100);
                    decimal tt1 = tt * item.SoLuong;
                    ctdh.DonGia = tt1;
                }
                db.ChiTietDonHangs.Add(ctdh);
                db.SaveChanges();
            }
            GuiEmail gm = new GuiEmail();
            gm.Emailnguoigui = "hphuy1206@gmail.com";
            gm.mathau = "adhk1997";
            gm.Emailnguoinhan = txtemail;
            string nd = "Cảm ơn quý khách đã đặt hàng tại Good-Market <br/> Mã đơn hàng của quý khách là : <b>" + dh.Ma +"</b> <br> Đơn hàng của quý khách gồm : <br>";
            var thanhtien = 0.0; var tongtien = 0.0; var VAT = 0.0; var tongcong = tongtien + VAT;
            foreach (var item in ghs)
            {
                if (item.Spham.KhuyenMai == null)
                {
                    thanhtien = item.SoLuong * (double)item.Spham.Gia;
                    tongtien = tongtien + thanhtien;
                }
                else
                {
                    double tt = Convert.ToDouble(item.Spham.Gia * (100 - item.Spham.KhuyenMai) / 100);
                    double tt1 = tt * item.SoLuong;
                    tongtien = tongtien + tt1;
                }
                
                if(item.Spham.KhuyenMai==null)
                {
                    nd += item.Spham.Ten + ":" + item.Spham.Gia + "đ" + "<br>";                  
                }
                  else
                {
                    double gia = Convert.ToDouble(item.Spham.Gia * (100 - item.Spham.KhuyenMai) / 100);
                    double gia1 = gia * item.SoLuong;
                    nd += item.Spham.Ten + ":" + gia1 + "đ" + "<br>";
                }             
            }
            VAT = tongtien / 10; tongcong = tongtien + VAT;
            nd += "VAT :"+ VAT+"<br>" + "Tổng tiền thanh toán :" + tongcong+"đ";
            gm.noidung = nd;
            gm.chude = "Good-Market : Xác nhận đơn hàng";
            gm.guithu();
            return RedirectToAction("dathangthanhcong", new { madh = dh.Ma });
        }
        public ActionResult dathangthanhcong(int madh)
        {
            var qr = db.DonHangs.SingleOrDefault(s => s.Ma == madh);
            return View(qr);
        }
    }
}