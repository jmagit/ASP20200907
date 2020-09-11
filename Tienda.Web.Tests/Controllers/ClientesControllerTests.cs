using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tienda.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Domain.Services.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Tienda.Web.Controllers.Tests {
    [TestClass()]
    public class ClientesControllerTests {
        [TestMethod()]
        public void ClientesControllerTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task IndexTest() {
            var lst = new[] { 
                new Customer(), new Customer(), new Customer(), 
                new Customer(), new Customer(), new Customer() 
            };
            var mock = new Mock<ICustomerDomainService>();
            mock.Setup(m => m.GetAll()).Returns(new List<Customer>(lst)); ;
            mock.Setup(m => m.GetPage(2, 5)).Returns(new List<Customer>(lst)); ;
            var contr = new ClientesController(mock.Object);

            var rslt = await contr.Index(2, 5) as ViewResult;
            Assert.AreEqual(2, rslt.ViewData["PagActual"]);
            Assert.AreEqual(1, rslt.ViewData["UltimaPagina"]);
            Assert.AreEqual(6, (rslt.Model as IEnumerable<Customer>).Count());
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