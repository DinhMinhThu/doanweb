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
    public class ChuyenMucsController : Controller
    {
        private KLTNEntities db = new KLTNEntities();
        // GET: Admin/ChuyenMucs
        public ActionResult Timkiem(int? page)
        {

            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //var sPs = db.SPs.Include(s => s.LoaiSP);
            return View(db.ChuyenMucs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Timkiem(int? page, string keyword)
        {
            var dssp = db.ChuyenMucs.Where(s => s.Ten.Contains(keyword)).ToList();
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
                return View(db.ChuyenMucs.ToList());
            }
            return RedirectToAction("Login", "Default");
        }

        // GET: admin/ChuyenMucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMuc);
        }

        // GET: admin/ChuyenMucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ChuyenMucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Ma,Ten,Mota,MaCT")] ChuyenMuc chuyenMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenMuc).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuyenMuc);
        }

        // GET: admin/ChuyenMucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ma = new SelectList(db.TinTucs, "Ma", "MotaNgan", chuyenMuc.Ma);
            return View(chuyenMuc);
        }

        // POST: admin/ChuyenMucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma,Ten,Mota,MaCT")] ChuyenMuc chuyenMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuyenMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ma = new SelectList(db.TinTucs, "Ma", "MotaNgan", chuyenMuc.Ma);
            return View(chuyenMuc);
        }

        // GET: admin/ChuyenMucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            if (chuyenMuc == null)
            {
                return HttpNotFound();
            }
            return View(chuyenMuc);
        }

        // POST: admin/ChuyenMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuyenMuc chuyenMuc = db.ChuyenMucs.Find(id);
            db.ChuyenMucs.Remove(chuyenMuc);
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