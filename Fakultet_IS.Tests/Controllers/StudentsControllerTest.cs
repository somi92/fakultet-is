using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fakultet_IS;
using Fakultet_IS.Controllers;

namespace Fakultet_IS.Tests.Controllers
{
    [TestClass]
    public class StudentsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            StudentsController controller = new StudentsController();
            ViewResult result = controller.Index("", "test", "test", 1) as ViewResult;
            
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("", result.ViewBag.CurrentSort);
            Assert.AreEqual("name_desc", result.ViewBag.NameSortParm);
            Assert.AreEqual("BI", result.ViewBag.BISortParm);
            Assert.AreEqual("city", result.ViewBag.CitySortParm);
        }
    }
}
