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
using Xunit;

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
        public void Test_Available_Parking_Spots()
        {
            int expected = 98;

            ParkWhereController.PlateDto plateDto = new ParkWhereController.PlateDto
            {
                Plate = "AB12345",
                time = DateTime.Now
            };

            ActionResult<int> actionResult = _parkWhereController.Available_Parking_Spots(plateDto);

            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result, "Resultatet var null - forventede OkObjectResult");

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void GetParkingSpots()
        {
            int expected = 99;
            ActionResult<int> actual = _parkWhereController.GetAvailable();
            
            var actualResult = actual.Result as OkObjectResult;

            Assert.AreEqual(expected, actualResult.Value);
        }

    }
}