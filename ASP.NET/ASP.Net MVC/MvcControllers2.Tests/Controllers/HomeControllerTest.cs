using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcControllers2;
using MvcControllers2.Controllers;
using MvcControllers2.Tests.TestDoubles;

namespace MvcControllers2.Tests.Controllers
{
    [TestClass]
    public class When_HelloController_Executes
    {
        [TestMethod]
        public void Write_To_The_Log()
        {
            var logger = new FakeLogger();

            var controller = new HelloController(logger);
            controller.Execute(new FakeRequestContext());

            Assert.IsNotNull(logger.LogResult);
            Assert.IsTrue(logger.LogResult.Length > 0);
        }
    }
    //[TestClass]
    //public class HomeControllerTest
    //{
    //    [TestMethod]
    //    public void Index()
    //    {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.Index() as ViewResult;

    //        // Assert
    //        Assert.IsNotNull(result);
    //    }

    //    [TestMethod]
    //    public void About()
    //    {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.About() as ViewResult;

    //        // Assert
    //        Assert.AreEqual("Your application description page.", result.ViewBag.Message);
    //    }

    //    [TestMethod]
    //    public void Contact()
    //    {
    //        // Arrange
    //        HomeController controller = new HomeController();

    //        // Act
    //        ViewResult result = controller.Contact() as ViewResult;

    //        // Assert
    //        Assert.IsNotNull(result);
    //    }
    //}
}
