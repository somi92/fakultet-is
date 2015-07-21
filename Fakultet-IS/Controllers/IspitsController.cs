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
using Fakultet_IS.DAL;

namespace Fakultet_IS.Controllers
{
    public class IspitsController : Controller
    {
        private UnitOfWork unitOfWork;

        public IspitsController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public IspitsController(IFakultetRepository<Ispits> ispitRepository)
        {
            this.unitOfWork = new UnitOfWork();
            this.unitOfWork.IspitsRepository = ispitRepository;
        }

        // GET: Ispits
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "ID" ? "id_desc" : "ID";
            var ispits = from s in unitOfWork.IspitsRepository.GetEntities()
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
            Ispits ispits = unitOfWork.IspitsRepository.GetEntityById(id);
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
                unitOfWork.IspitsRepository.InsertEntity(ispits);
                unitOfWork.Save();
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
            Ispits ispits = unitOfWork.IspitsRepository.GetEntityById(id);
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
                unitOfWork.IspitsRepository.UpdateEntity(ispits);
                unitOfWork.Save();
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
            Ispits ispits = unitOfWork.IspitsRepository.GetEntityById(id);
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
            Ispits ispits = unitOfWork.IspitsRepository.GetEntityById(id);
            unitOfWork.IspitsRepository.DeleteEntity(ispits);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
