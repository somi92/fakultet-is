using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fakultet_IS.Models;
using PagedList;

namespace Fakultet_IS.Controllers
{
    public class IspitsController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: Ispits
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "ID" ? "id_desc" : "ID";
            var ispits = from s in db.Ispits
                           select s;

            switch (sortOrder)
            {
                case "name_desc":
                    ispits = ispits.OrderByDescending(s => s.Naziv);
                    break;
                case "ID":
                    ispits = ispits.OrderBy(s => s.IspitID);
                    break;
                case "id_desc":
                    ispits = ispits.OrderByDescending(s => s.IspitID);
                    break;
                default:
                    ispits = ispits.OrderBy(s => s.Naziv);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(ispits.ToPagedList(pageNumber, pageSize));
        }

        // GET: Ispits/Details/5
        public ActionResult Details(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ispits ispits = db.Ispits.Find(id);
            if (ispits == null)
            {
                return HttpNotFound();
            }
            return View(ispits);
        }

        // GET: Ispits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ispits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IspitID,Naziv")] Ispits ispits)
        {
            if (ModelState.IsValid)
            {
                db.Ispits.Add(ispits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ispits);
        }

        // GET: Ispits/Edit/5
        public ActionResult Edit(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ispits ispits = db.Ispits.Find(id);
            if (ispits == null)
            {
                return HttpNotFound();
            }
            return View(ispits);
        }

        // POST: Ispits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IspitID,Naziv")] Ispits ispits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ispits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ispits);
        }

        // GET: Ispits/Delete/5
        public ActionResult Delete(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ispits ispits = db.Ispits.Find(id);
            if (ispits == null)
            {
                return HttpNotFound();
            }
            return View(ispits);
        }

        // POST: Ispits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(double id)
        {
            Ispits ispits = db.Ispits.Find(id);
            db.Ispits.Remove(ispits);
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
