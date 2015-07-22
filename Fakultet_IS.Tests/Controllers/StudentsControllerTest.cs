using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fakultet_IS;
using Fakultet_IS.Controllers;
using Moq;
using Fakultet_IS.Models;
using Fakultet_IS.DAL;
using PagedList;

namespace Fakultet_IS.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTest
    {

        private StudentsController controller;

        [TestMethod]
        public void Index()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            var students = new List<Students>();
            students.Add(new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1"});
            students.Add(new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" });
            
            repMock.Setup(x => x.GetEntities()).Returns(students.ToPagedList(1, 5));

            controller = new StudentsController(repMock.Object);
            ViewResult result = controller.Index("", "", "", 1) as ViewResult;
            var listResult = result.ViewData.Model as PagedList<Students>;
            var list = listResult.ToList();

            repMock.VerifyAll();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("", result.ViewBag.CurrentSort);
            Assert.AreEqual("name_desc", result.ViewBag.NameSortParm);
            Assert.AreEqual("BI", result.ViewBag.BISortParm);
            Assert.AreEqual("city", result.ViewBag.CitySortParm);
        }

        [TestMethod]
        public void Details()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };

            repMock.Setup(x => x.GetEntityById(student.BI)).Returns(student);
            controller = new StudentsController(repMock.Object);
            ViewResult result = controller.Details(student.BI) as ViewResult;
            repMock.VerifyAll();
            Students s = result.ViewData.Model as Students;
            Assert.IsNotNull(s);
            Assert.AreEqual("10011", s.BI);
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Create()
        {
            controller = new StudentsController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateStudentParam()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            repMock.Setup(x => x.InsertEntity(student));
            controller = new StudentsController(repMock.Object);
            var result = controller.Create(student) as RedirectToRouteResult; 
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void EditIdParam()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            repMock.Setup(x => x.GetEntityById(student.BI)).Returns(student);
            controller = new StudentsController(repMock.Object);
            var result = controller.Edit(student.BI) as ViewResult;
            Students s = result.ViewData.Model as Students;
            repMock.VerifyAll();
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual("10011", s.BI);
        }

        [TestMethod]
        public void EditStudentsParam() {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            repMock.Setup(x => x.UpdateEntity(student));
            controller = new StudentsController(repMock.Object);
            var result = controller.Edit(student) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void DeleteIdParam()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            repMock.Setup(x => x.GetEntityById(student.BI)).Returns(student);
            controller = new StudentsController(repMock.Object);
            var result = controller.Delete(student.BI) as ViewResult;
            Students s = result.ViewData.Model as Students;
            repMock.VerifyAll();
            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual("10011", s.BI);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            Students student = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            repMock.Setup(x => x.GetEntityById(student.BI)).Returns(student);
            controller = new StudentsController(repMock.Object);
            var result = controller.DeleteConfirmed(student.BI) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
    }
}
