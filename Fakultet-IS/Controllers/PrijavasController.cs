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
    public class PrijavasController : Controller
    {
        private FakultetEntities db = new FakultetEntities();

        // GET: Prijavas
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BISortParm = sortOrder == "BI" ? "bi_desc" : "BI";
            ViewBag.SubjectSortParm = sortOrder == "subj" ? "subj_desc" : "subj";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var prijavas = from s in db.Prijavas.Include(p => p.Ispits).Include(p => p.Students)
                               select s;

            if (!String.IsNullOrEmpty(search))
            {
                prijavas = prijavas.Where(s => s.Students.Prezime.Contains(search)
                                       || s.Students.Ime.Contains(search)
                                       || s.BI.Contains(search)
                                       || s.Ispits.Naziv.Contains(search));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    prijavas = prijavas.OrderByDescending(s => s.Students.Prezime);
                    break;
                case "BI":
                    prijavas = prijavas.OrderBy(s => s.BI);
                    break;
                case "bi_desc":
                    prijavas = prijavas.OrderByDescending(s => s.BI);
                    break;
                case "subj":
                    prijavas = prijavas.OrderBy(s => s.Ispits.Naziv);
                    break;
                case "subj_desc":
                    prijavas = prijavas.OrderByDescending(s => s.Ispits.Naziv);
                    break;
                default:
                    prijavas = prijavas.OrderBy(s => s.Students.Prezime);
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(prijavas.ToPagedList(pageNumber, pageSize));
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
