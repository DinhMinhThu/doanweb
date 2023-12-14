using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using KLTN_FreshShop.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace KLTN_FreshShop.Areas.Admin.Controllers
{
    public class SPsController : Controller
    {
        private KLTNEntities db = new KLTNEntities();
        // GET: Admin/SPs
        public ActionResult Timkiem(int? page)
        {

            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //var sPs = db.SPs.Include(s => s.LoaiSP);
            return View(db.SPs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Timkiem(int? page, string keyword)
        {
            var dssp = db.SPs.Where(s => s.Ten.Contains(keyword)).ToList();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(dssp.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Index(int? page)
        {
            if (Session["nd"] != null)
            {
                if (page == null)
                {
                    page = 1;
                }
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var sPs = db.SPs.Include(s => s.LoaiSP);
                return View(db.SPs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Login", "Default");

        }
        public PartialViewResult GetPaging(int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return PartialView(db.SPs.OrderBy(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        // GET: admin/SPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SP sP = db.SPs.Find(id);
            if (sP == null)
            {
                return HttpNotFound();
            }
            return View(sP);
        }

        // GET: admin/SPs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "Ma", "Ten");
            return View();
        }

        // POST: admin/SPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Ma,Ten,MoTa,MaLoai,AnhNho,Anh1,Anh2,Anh3,TSKT,TieuBieu,TrangThai,Xoa,Hang,Gia,KhuyenMai,NgayDang,DVT")] SP sP)
        {

            if (ModelState.IsValid)
            {
                db.SPs.Add(sP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "Ma", "Ten", sP.MaLoai);
            return View(sP);
        }

        // GET: admin/SPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SP sP = db.SPs.Find(id);
            if (sP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "Ma", "Ten", sP.MaLoai);
            return View(sP);
        }

        // POST: admin/SPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Ma,Ten,MoTa,MaLoai,AnhNho,Anh1,Anh2,Anh3,TSKT,TieuBieu,TrangThai,Xoa,Hang,Gia,KhuyenMai,NgayDang,DVT")] SP sP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiSPs, "Ma", "Ten", sP.MaLoai);
            return View(sP);
        }

        // GET: admin/SPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SP sP = db.SPs.Find(id);
            if (sP == null)
            {
                return HttpNotFound();
            }
            return View(sP);
        }

        // POST: admin/SPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SP sP = db.SPs.Find(id);
            db.SPs.Remove(sP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}