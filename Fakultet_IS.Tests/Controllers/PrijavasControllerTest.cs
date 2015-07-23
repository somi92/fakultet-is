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
    public class PrijavasControllerTest
    {

        private PrijavasController controller;

        [TestMethod]
        public void Index()
        {
            var repMock = new Mock<IFakultetRepository<Prijavas>>();
            var prijavas = new List<Prijavas>();
            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1"};
            Students s2 = new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Ispits i2 = new Ispits() { IspitID = 2, Naziv = "Ispit2" };
            prijavas.Add(new Prijavas() { BI = "10011", IspitID = 2, Ocena = 8, Students = s1, Ispits = i2});
            prijavas.Add(new Prijavas() { BI = "20011", IspitID = 1, Ocena = 9, Students = s2, Ispits = i1});

            repMock.Setup(x => x.GetEntities()).Returns(prijavas.ToPagedList(1, 5));

            controller = new PrijavasController(repMock.Object);
            ViewResult result = controller.Index("", "", "", 1) as ViewResult;
            var listResult = result.ViewData.Model as PagedList<Prijavas>;
            var list = listResult.ToList();

            repMock.VerifyAll();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("", result.ViewBag.CurrentSort);
            Assert.AreEqual("name_desc", result.ViewBag.NameSortParm);
            Assert.AreEqual("BI", result.ViewBag.BISortParm);
            Assert.AreEqual("subj", result.ViewBag.SubjectSortParm);
        }

        [TestMethod]
        public void Details()
        {
            var repMock = new Mock<IFakultetRepository<Prijavas>>();
            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Prijavas prijava = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };

            object[] con = {prijava.BI, prijava.IspitID};
            repMock.Setup(x => x.GetEntityById(con)).Returns(prijava);
            controller = new PrijavasController(repMock.Object);
            ViewResult result = controller.Details(prijava.BI, prijava.IspitID+"") as ViewResult;
            repMock.VerifyAll();
            Prijavas p = result.ViewData.Model as Prijavas;
            Assert.IsNotNull(p);
            Assert.AreEqual("10011", p.BI);
            Assert.AreEqual(1, p.IspitID);
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Create()
        {
            var repMockIsp = new Mock<IFakultetRepository<Ispits>>();
            var repMockStud = repMockIsp.As<IFakultetRepository<Students>>();

            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Students s2 = new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Ispits i2 = new Ispits() { IspitID = 2, Naziv = "Ispit2" };
            var studentList = new List<Students>();
            studentList.Add(s1);
            studentList.Add(s2);
            var ispitList = new List<Ispits>();
            ispitList.Add(i1);
            ispitList.Add(i2);
            repMockStud.Setup(x => x.GetEntities()).Returns(studentList);
            repMockIsp.Setup(x => x.GetEntities()).Returns(ispitList);

            controller = new PrijavasController();
            controller.SetStudentsRepository(repMockStud.Object);
            controller.SetIspitsRepository(repMockIsp.Object);
            var result = controller.Create();
            ViewResult viewRes = result as ViewResult;
            var selectList = viewRes.ViewBag.BI as SelectList;

            repMockIsp.VerifyAll();
            repMockStud.VerifyAll();

            Assert.AreEqual("Create", viewRes.ViewName);
            Assert.AreEqual(selectList.ElementAt(0).Text, s1.BI+" - "+s1.Ime+" "+s1.Prezime);
            Assert.AreEqual(selectList.ElementAt(1).Text, s2.BI + " - " + s2.Ime + " " + s2.Prezime);
        }

        [TestMethod]
        public void CreatePrijavaParam()
        {
            var repMock = new Mock<IFakultetRepository<Prijavas>>();

            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Prijavas prijavas = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };
            repMock.Setup(x => x.InsertEntity(prijavas));
            controller = new PrijavasController(repMock.Object);
            var result = controller.Create(prijavas) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        
        [TestMethod]
        public void EditIdParam()
        {
            var repMockPri = new Mock<IFakultetRepository<Prijavas>>();
            var repMockStud = new Mock<IFakultetRepository<Students>>();
            var repMockIsp = new Mock<IFakultetRepository<Ispits>>();

            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Students s2 = new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Ispits i2 = new Ispits() { IspitID = 2, Naziv = "Ispit2" };
            Prijavas prijavas = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };
            var studentList = new List<Students>();
            studentList.Add(s1);
            studentList.Add(s2);
            var ispitList = new List<Ispits>();
            ispitList.Add(i1);
            ispitList.Add(i2);

            object[] con = { prijavas.BI, prijavas.IspitID };
            repMockPri.Setup(x => x.GetEntityById(con)).Returns(prijavas);
            repMockStud.Setup(x => x.GetEntities()).Returns(studentList);
            repMockIsp.Setup(x => x.GetEntities()).Returns(ispitList);

            controller = new PrijavasController(repMockPri.Object);
            controller.SetIspitsRepository(repMockIsp.Object);
            controller.SetStudentsRepository(repMockStud.Object);

            var result = controller.Edit(prijavas.BI, prijavas.IspitID + "") as ViewResult;
            var sList = result.ViewBag.BI as SelectList;
            var iList = result.ViewBag.IspitID as SelectList;

            repMockIsp.VerifyAll();
            repMockPri.VerifyAll();
            repMockStud.VerifyAll();

            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(sList.ElementAt(0).Value, "10011");
            Assert.AreEqual(sList.ElementAt(1).Value, "20011");
            Assert.AreEqual(iList.ElementAt(0).Value, 1+"");
            Assert.AreEqual(iList.ElementAt(1).Value, 2+"");
        }

        [TestMethod]
        public void EditPrijavasParam()
        {
            var repMockPri = new Mock<IFakultetRepository<Prijavas>>();
            var repMockStud = new Mock<IFakultetRepository<Students>>();
            var repMockIsp = new Mock<IFakultetRepository<Ispits>>();

            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Students s2 = new Students() { BI = "20011", Ime = "Marko", Prezime = "Markovic", Adresa = "Ulica2", Grad = "Grad2" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Ispits i2 = new Ispits() { IspitID = 2, Naziv = "Ispit2" };
            Prijavas prijavas = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };

            repMockPri.Setup(x => x.UpdateEntity(prijavas));

            controller = new PrijavasController(repMockPri.Object);
            controller.SetIspitsRepository(repMockIsp.Object);
            controller.SetStudentsRepository(repMockStud.Object);

            var result = controller.Edit(prijavas);
            var viewResult = result as ViewResult;
            var rtaResult = result as RedirectToRouteResult;

            repMockIsp.VerifyAll();
            repMockPri.VerifyAll();
            repMockStud.VerifyAll();

            Assert.AreEqual("Index", rtaResult.RouteValues["Action"]);
        }

        [TestMethod]
        public void DeleteIdParam()
        {
            var repMock = new Mock<IFakultetRepository<Prijavas>>();
            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Prijavas prijavas = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };
            object[] con = { prijavas.BI, prijavas.IspitID };
            repMock.Setup(x => x.GetEntityById(con)).Returns(prijavas);
            controller = new PrijavasController(repMock.Object);
            var result = controller.Delete(prijavas.BI, prijavas.IspitID + "") as ViewResult;
            Prijavas p = result.ViewData.Model as Prijavas;
            repMock.VerifyAll();
            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual("10011-1", p.BI+"-"+p.IspitID);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            var repMock = new Mock<IFakultetRepository<Prijavas>>();
            Students s1 = new Students() { BI = "10011", Ime = "Pera", Prezime = "Peric", Adresa = "Ulica1", Grad = "Grad1" };
            Ispits i1 = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            Prijavas prijavas = new Prijavas() { BI = "10011", IspitID = 1, Ocena = 8, Students = s1, Ispits = i1 };
            object[] con = { prijavas.BI, prijavas.IspitID };
            repMock.Setup(x => x.GetEntityById(con)).Returns(prijavas);
            controller = new PrijavasController(repMock.Object);
            var result = controller.DeleteConfirmed(prijavas.BI, prijavas.IspitID + "") as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
    }
}
