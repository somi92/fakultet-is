﻿using System;
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
    public class StudentsController : Controller
    {
        private UnitOfWork unitOfWork;

        public StudentsController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public StudentsController(IFakultetRepository<Students> studentRepository)
        {
            this.unitOfWork = new UnitOfWork();
            this.unitOfWork.StudentsRepository = studentRepository;
        }

        // GET: Students
        public ActionResult Index(string sortOrder, string currentFilter, string search, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.BISortParm = sortOrder == "BI" ? "bi_desc" : "BI";
            ViewBag.CitySortParm = sortOrder == "city" ? "city_desc" : "city";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;


            var students = from s in unitOfWork.StudentsRepository.GetEntities()
                           select s;

            if (!String.IsNullOrEmpty(search))
            {
                students = students.Where(s => s.Prezime.Contains(search)
                                       || s.Ime.Contains(search)
                                       || s.BI.Contains(search));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Prezime);
                    break;
                case "BI":
                    students = students.OrderBy(s => s.BI);
                    break;
                case "bi_desc":
                    students = students.OrderByDescending(s => s.BI);
                    break;
                case "city":
                    students = students.OrderBy(s => s.Grad);
                    break;
                case "city_desc":
                    students = students.OrderByDescending(s => s.Grad);
                    break;
                default:
                    students = students.OrderBy(s => s.Prezime);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View("Index", students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = unitOfWork.StudentsRepository.GetEntityById(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BI,Ime,Prezime,Adresa,Grad")] Students students)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.StudentsRepository.InsertEntity(students);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(students);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = unitOfWork.StudentsRepository.GetEntityById(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BI,Ime,Prezime,Adresa,Grad")] Students students)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.StudentsRepository.UpdateEntity(students);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = unitOfWork.StudentsRepository.GetEntityById(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Students students = unitOfWork.StudentsRepository.GetEntityById(id);
            unitOfWork.StudentsRepository.DeleteEntity(id);
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
