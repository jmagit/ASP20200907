using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tienda.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Domain.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Tienda.Web.Controllers.Tests {
    [TestClass()]
    public class ClientesControllerTests {
        [TestMethod()]
        public void ClientesControllerTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public async void IndexTest() {
            var mock = new Mock<ICustomerDomainService>();
            mock.Setup(m => m.GetPage(0, 30)).Returns(new List<Customer>()); ;
            var contr = new ClientesController(mock.Object);

            var rslt = await contr.Index(0, 30) as ViewResult;
            Assert.AreEqual(0, rslt.ViewData["PagActual"]);
        }

        [TestMethod()]
        public void AjaxConPartialViewTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void PaginaTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void AjaxConJSONTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void DetailsTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1() {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1() {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void NewpwdTest() {
            Assert.Fail();
        }
    }
}