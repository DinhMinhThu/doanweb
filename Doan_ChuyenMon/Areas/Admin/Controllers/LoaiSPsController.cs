using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using Doan_ChuyenMon.Models;
using PagedList;

namespace Doan_ChuyenMon.Areas.Admin.Controllers
{
    public class LoaiSPsController : Controller
    {
        private DoAn_ChuyenMonEntities db = new DoAn_ChuyenMonEntities();
        // GET: Admin/LoaiSP
        public ActionResult Timkiem(int? page)
        {

            if (page == null)
            {
                page = 1;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            //var sPs = db.SPs.Include(s => s.LoaiSP);
            return View(db.LoaiSPs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Timkiem(int? page, string keyword)
        {
            var dssp = db.LoaiSPs.Where(s => s.Ten.Contains(keyword)).ToList();
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
                //var sps = db.LoaiSPs.Include(s => s.Ma);
                return View(db.LoaiSPs.OrderByDescending(s => s.Ma).ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Login", "Default");

        }
        // GET: admin/LoaiSPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // GET: admin/LoaiSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/LoaiSPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ma,Ten,Mota")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSPs.Add(loaiSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSP);
        }

        // GET: admin/LoaiSPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: admin/LoaiSPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma,Ten,Mota")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSP);
        }

        // GET: admin/LoaiSPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: admin/LoaiSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            db.LoaiSPs.Remove(loaiSP);
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