using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastFood;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FastFood.Controllers;
using FastFood.Models;

namespace FastFood
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HomeController home = new HomeController();
            ViewResult result = home.Index() as ViewResult;
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void CheckLoggedIn()
        {
            HomeController home = new HomeController();
            String result = home.ValidateUser("2", "123456");

            Assert.AreEqual("0", result);

        }
        [TestMethod]
        public void CheckUserExist()
        {
            String userid = "1";
            HomeController home = new HomeController();
            bool result = home.userExist("1");
            Assert.AreEqual(true, result);

        }
        [TestMethod]
        public void Check()
        {
            HomeController home = new HomeController();
            ViewResult view = home.Foods() as ViewResult;

        }

    }
}
