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
            // 1. Setup In-Memory Database
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new MyDbContext(options);
            _carService = new GenericDbService<Car>(options);

            // Seed med eksisterende bil (AB12345) så den test ikke rammer API
            _context.ParkingEvents.Add(new ParkingEvent { LicensePlate = "AB12345" });
            _context.SaveChanges();

            // 2. Setup af HttpClient (Simulerer din appsettings konfiguration)
            var client = new HttpClient();

            // Konfiguration ifølge https://v1.motorapi.dk/doc/
            client.BaseAddress = new Uri("https://v1.motorapi.dk/");

            // --- OBS: INDSÆT DIN RIGTIGE NØGLE HERUNDER ---
            client.DefaultRequestHeaders.Add("X-Auth-Token", "mllr9po25fx4ylvtymlvwfoqxmxdh9rx");

            _mockHttpClientFactory = new Mock<IHttpClientFactory>();

            // Vi opsætter mocken til at reagere specifikt på navnet "MotorApi"
            // ligesom i din controller: _httpClient = httpClientFactory.CreateClient("MotorApi");
            _mockHttpClientFactory.Setup(x => x.CreateClient("MotorApi"))
                                  .Returns(client);

            // 3. Setup ParkingLot Mock
            _mockParkingLot = new Mock<IParkingLot>();
            _mockParkingLot.Setup(x => x.EventTrigger(It.IsAny<string>(), It.IsAny<DateTime>()))
                           .Returns(98);
            _mockParkingLot.Setup(x => x.GetAvailableSpaces())
                           .Returns(99);

            // 4. Start Controlleren
            _controller = new ParkWhereController(
                _mockParkingLot.Object,
                _mockHttpClientFactory.Object,
                _carService,
                _context
            );
        }

        [TestMethod()]
        public void ChangeParkingSpotAmountTest()
        {
            // Eksisterende bil (AB12345) -> Ingen API kald
            int expected = 98;
            var plateDto = new ParkWhereController.PlateDto { Plate = "AB12345", Time = DateTime.Now };

            var actionResult = _controller.ChangeParkingSpotAmount(plateDto).Result;
            var result = actionResult as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Value);
        }

        //Denne test er udkommenteret da den bruger 1 API kald pr kørt test, dog virker den som den skal,
        //så blot indkommenter og kør den hvis det skal vises.

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