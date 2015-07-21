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
        [TestMethod]
        public void Index()
        {
            var repMock = new Mock<IFakultetRepository<Students>>();
            var students = new List<Students>();
            students.Add(new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1"});
            students.Add(new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" });
            
            repMock.Setup(x => x.GetEntities()).Returns(students.ToPagedList(1, 5));

            StudentsController controller = new StudentsController(repMock.Object);
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
    }
}
