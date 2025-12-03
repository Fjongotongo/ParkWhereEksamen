using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private HttpClient _httpClient;

        private ParkingLot _parkingLot;
        private ParkWhereController _parkWhereController;

        [TestInitialize]
        public void TestInitialize()
        {
            _parkingLot = new ParkingLot()
            {
                ParkingSpaces = 100,
                CarsParked = 1,
                AvailableSpaces = 99
            };
            _parkWhereController = new ParkWhereController(_parkingLot);
        }

        [TestMethod()]
        public void Test_Get_Available_Parking_Spots()
        {
            int expected = 98;

            ParkWhereController.PlateDto plateDto = new ParkWhereController.PlateDto
            {
                Plate = "AB12345",
                time = DateTime.Now
            };
            ActionResult<int> result = _parkWhereController.Available_Parking_Spots(plateDto);
            Assert.AreEqual(expected, (int)result.Value);
        } 

        //[TestMethod()]
        //public void GetAllTest()
        //{
        //    var cars = _controller.GetAll();



        //}

        //[TestMethod()]
        //public void GetTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void PostTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ReceivePlateTest()
        //{
        //    Assert.Fail();
        //}
    }
}