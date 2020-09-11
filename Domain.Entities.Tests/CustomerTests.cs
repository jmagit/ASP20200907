using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tests {
    [TestClass()]
    public class CustomerTests {
        [TestMethod()]
        public void ValidateTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CustomerTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidateTest1() {
            Assert.Fail();
        }

        [TestMethod()]
        public void CambiaContraseñaTest() {
            var c = new Customer();

            c.CambiaContraseña("P@$$w0rd");
                        
            Assert.IsTrue(c.EsValidaLaContraseña("P@$$w0rd"));
        }

        [TestMethod()]
        public void EsValidaLaContraseñaTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHashPasswordRfc2898Test() {
            Assert.Fail();
        }
    }
}