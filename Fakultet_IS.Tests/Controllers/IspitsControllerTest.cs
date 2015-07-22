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
    public class IspitsControllerTest
    {

        private IspitsController controller;

        [TestMethod]
        public void Index()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            var ispits = new List<Ispits>();
            ispits.Add(new Ispits() { IspitID = 1, Naziv = "Ispit1" });
            ispits.Add(new Ispits() { IspitID = 2, Naziv = "Ispit2" });

            repMock.Setup(x => x.GetEntities()).Returns(ispits.ToPagedList(1, 5));

            controller = new IspitsController(repMock.Object);
            ViewResult result = controller.Index("", 1) as ViewResult;
            var listResult = result.ViewData.Model as PagedList<Ispits>;
            var list = listResult.ToList();

            repMock.VerifyAll();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("", result.ViewBag.CurrentSort);
            Assert.AreEqual("name_desc", result.ViewBag.NameSortParm);
            Assert.AreEqual("ID", result.ViewBag.IDSortParm);
        }

        [TestMethod]
        public void Details()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1"};

            repMock.Setup(x => x.GetEntityById(ispit.IspitID)).Returns(ispit);
            controller = new IspitsController(repMock.Object);
            ViewResult result = controller.Details(ispit.IspitID) as ViewResult;
            repMock.VerifyAll();
            Ispits i = result.ViewData.Model as Ispits;
            Assert.IsNotNull(i);
            Assert.AreEqual(1, ispit.IspitID);
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void Create()
        {
            controller = new IspitsController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateIspitParam()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            repMock.Setup(x => x.InsertEntity(ispit));
            controller = new IspitsController(repMock.Object);
            var result = controller.Create(ispit) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void EditIdParam()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            repMock.Setup(x => x.GetEntityById(ispit.IspitID)).Returns(ispit);
            controller = new IspitsController(repMock.Object);
            var result = controller.Edit(ispit.IspitID) as ViewResult;
            Ispits i = result.ViewData.Model as Ispits;
            repMock.VerifyAll();
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(1, i.IspitID);
        }

        [TestMethod]
        public void EditIspitsParam()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            repMock.Setup(x => x.UpdateEntity(ispit));
            controller = new IspitsController(repMock.Object);
            var result = controller.Edit(ispit) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void DeleteIdParam()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            repMock.Setup(x => x.GetEntityById(ispit.IspitID)).Returns(ispit);
            controller = new IspitsController(repMock.Object);
            var result = controller.Delete(ispit.IspitID) as ViewResult;
            Ispits i = result.ViewData.Model as Ispits;
            repMock.VerifyAll();
            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual(1, i.IspitID);
        }

        [TestMethod]
        public void DeleteConfirmed()
        {
            var repMock = new Mock<IFakultetRepository<Ispits>>();
            Ispits ispit = new Ispits() { IspitID = 1, Naziv = "Ispit1" };
            repMock.Setup(x => x.GetEntityById(ispit.IspitID)).Returns(ispit);
            controller = new IspitsController(repMock.Object);
            var result = controller.DeleteConfirmed(ispit.IspitID) as RedirectToRouteResult;
            repMock.VerifyAll();
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
    }
}
