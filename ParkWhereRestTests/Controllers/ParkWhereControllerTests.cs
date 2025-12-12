using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkWhereLib;
using ParkWhereLib.DbService;
using ParkWhereLib.Models;
using ParkWhereRest.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkWhereRest.Controllers.Tests
{
    [TestClass()]
    public class ParkWhereControllerTests
    {
        private Mock<IParkingLot> _mockParkingLot;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private GenericDbService<Car> _carService;
        private MyDbContext _context;
        private ParkWhereController _controller;

        [TestInitialize]
        public void TestInitialize()
        {
            //Setup In-Memory Database
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new MyDbContext(options);
            _carService = new GenericDbService<Car>(options);

            // Seeding with existing car (AB12345) to make sure the Api wont reach it
            _context.ParkingEvents.Add(new ParkingEvent { LicensePlate = "AB12345" });
            _context.SaveChanges();

            //Setting up HttpClient
            var client = new HttpClient();

            // Configuration https://v1.motorapi.dk/doc/
            client.BaseAddress = new Uri("https://v1.motorapi.dk/");

            client.DefaultRequestHeaders.Add("X-Auth-Token", "mllr9po25fx4ylvtymlvwfoqxmxdh9rx");

            _mockHttpClientFactory = new Mock<IHttpClientFactory>();

            // Setting up the mock to react specifically to the name "MotorApi"
            _mockHttpClientFactory.Setup(x => x.CreateClient("MotorApi"))
                                  .Returns(client);

            // Setup ParkingLot Mock
            _mockParkingLot = new Mock<IParkingLot>();
            _mockParkingLot.Setup(x => x.EventTrigger(It.IsAny<string>(), It.IsAny<DateTime>()))
                           .Returns(98);
            _mockParkingLot.Setup(x => x.GetAvailableSpaces())
                           .Returns(99);

            _controller = new ParkWhereController(
                _mockParkingLot.Object,
                _mockHttpClientFactory.Object,
                _carService,
                _context
            );
        }

        /// <summary>
        /// Tests that the ParkWhereController.ChangeParkingSpotAmount method correctly updates the number
        /// of available parking spots when provided with valid plate information.
        /// </summary>
        [TestMethod()]
        public void ChangeParkingSpotAmountTest()
        {
            int expected = 98;
            var plateDto = new ParkWhereController.PlateDto { Plate = "AB12345", Time = DateTime.Now };

            var actionResult = _controller.ChangeParkingSpotAmount(plateDto).Result;
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
        }

        //This test is outcommented because it uses an Api call everytime the test runs.
        //It works as it should, remove it as comment if you want to test it

        //[TestMethod()]
        //public void ChangeParkingSpotAmountTests()
        //{
        //    int expected = 98;
        //    var plateDto = new ParkWhereController.PlateDto { Plate = "DF12345", Time = DateTime.Now };

        //    var actionResult = _controller.ChangeParkingSpotAmount(plateDto).Result;
        //    var result = actionResult as OkObjectResult;

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(expected, result.Value);
        //}


        /// <summary>
        /// Tests whether the GetAvailable method of the controller returns the expected number of available
        /// parking spots.
        /// </summary>
        [TestMethod()]
        public void GetParkingSpots()
        {
            int expected = 99;
            var actionResult = _controller.GetAvailable().Result;
            var result = actionResult as OkObjectResult;

            Assert.AreEqual(expected, result.Value);
        }
    }
}