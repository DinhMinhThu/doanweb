using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Doan_ChuyenMon.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace Doan_ChuyenMon.Areas.Admin.Controllers
{
    public class TinTucsController : Controller
    {
        private DoAn_ChuyenMonEntities db = new DoAn_ChuyenMonEntities();
        // GET: Admin/TinTucs
        public ActionResult Timkiem(int? page)
        {

            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //var sPs = db.SPs.Include(s => s.LoaiSP);
            return View(db.TinTucs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Timkiem(int? page, string keyword)
        {
            var dssp = db.TinTucs.Where(s => s.MotaNgan.Contains(keyword)).ToList();
            ViewBag.dem = dssp.Count();
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(dssp.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Index()
        {
            if (Session["nd"] != null)
            {
                var tinTucs = db.TinTucs.Include(t => t.ChuyenMuc);
                return View(tinTucs.ToList());
            }
            return RedirectToAction("Login", "Default");

        }

        // GET: admin/TinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: admin/TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.MaCM = new SelectList(db.ChuyenMucs, "Ma", "Ten");
            return View();
        }

        // POST: admin/TinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Ma,MaCM,MotaNgan,Mota,NgayDang,Anh,NguoiDang,TieuBieu,LoaiTin")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCM = new SelectList(db.ChuyenMucs, "Ma", "Ten", tinTuc.MaCM);
            return View(tinTuc);
        }

        // GET: admin/TinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMucs, "Ma", "Ten", tinTuc.MaCM);
            return View(tinTuc);
        }

        // POST: admin/TinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Ma,MaCM,MotaNgan,Mota,NgayDang,Anh,NguoiDang,TieuBieu,LoaiTin")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCM = new SelectList(db.ChuyenMucs, "Ma", "Ten", tinTuc.MaCM);
            return View(tinTuc);
        }

        // GET: admin/TinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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