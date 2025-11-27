using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkWhereLib;
using ParkWhereRest.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereRest.Controllers.Tests
{
    [TestClass()]
    public class ParkWhereControllerTests
    {
        private ParkWhereController _controller;
        private IParkWhereRepo _repo;
        private HttpClient _httpClient;


        [TestInitialize]
        public void TestInitialize()
        {
            _controller = new ParkWhereController(_repo, _httpClient);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var cars = _controller.GetAll();



        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReceivePlateTest()
        {
            Assert.Fail();
        }
    }
}