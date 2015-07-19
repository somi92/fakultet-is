using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fakultet_IS.Models;

namespace Fakultet_IS.Controllers
{
    public class PrijavasController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: Prijavas
        public ActionResult Index()
        {
            var prijavas = db.Prijavas.Include(p => p.Ispits).Include(p => p.Students);
            return View(prijavas.ToList());
        }

        // GET: Prijavas/Details/5
        public ActionResult Details(string bi, string ispit)
        {
            if (bi == null || ispit == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            double ispitID = Convert.ToDouble(ispit);
            object[] con = {bi, ispitID};
            Prijavas prijavas = db.Prijavas.Find(con);
            if (prijavas == null)
            {
                return HttpNotFound();
            }
            return View(prijavas);
        }

        // GET: Prijavas/Create
        public ActionResult Create()
        {
            ViewBag.IspitID = new SelectList(db.Ispits, "IspitID", "Naziv");
            var students = db.Students;
            List<object> studentsList = new List<object>();
            foreach (var student in students)
                studentsList.Add(new
                {
                    BI = student.BI,
                    BIImePrezime = student.BI + " - " + student.Ime + " " + student.Prezime
                });
            ViewBag.BI = new SelectList(studentsList, "BI", "BIImePrezime");
            return View();
        }

        // POST: Prijavas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BI,IspitID,Ocena")] Prijavas prijavas)
        {
            if (ModelState.IsValid)
            {
                db.Prijavas.Add(prijavas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IspitID = new SelectList(db.Ispits, "IspitID", "Naziv", prijavas.IspitID);
            ViewBag.BI = new SelectList(db.Students, "BI", "Ime", prijavas.BI);
            return View(prijavas);
        }

        // GET: Prijavas/Edit/5
        public ActionResult Edit(string bi, string ispit)
        {
            if (bi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            double ispitID = Convert.ToDouble(ispit);
            object[] con = { bi, ispitID };
            Prijavas prijavas = db.Prijavas.Find(con);
            if (prijavas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IspitID = new SelectList(db.Ispits, "IspitID", "Naziv", prijavas.IspitID);
            ViewBag.BI = new SelectList(db.Students, "BI", "Ime", prijavas.BI);
            return View(prijavas);
        }

        // POST: Prijavas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BI,IspitID,Ocena")] Prijavas prijavas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prijavas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IspitID = new SelectList(db.Ispits, "IspitID", "Naziv", prijavas.IspitID);
            ViewBag.BI = new SelectList(db.Students, "BI", "Ime", prijavas.BI);
            return View(prijavas);
        }

        // GET: Prijavas/Delete/5
        public ActionResult Delete(string bi, string ispit)
        {
            if (bi == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            double ispitID = Convert.ToDouble(ispit);
            object[] con = { bi, ispitID };
            Prijavas prijavas = db.Prijavas.Find(con);
            if (prijavas == null)
            {
                return HttpNotFound();
            }
            return View(prijavas);
        }

        // POST: Prijavas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string bi, string ispit)
        {
            double ispitID = Convert.ToDouble(ispit);
            object[] con = { bi, ispitID };
            Prijavas prijavas = db.Prijavas.Find(con);
            db.Prijavas.Remove(prijavas);
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
