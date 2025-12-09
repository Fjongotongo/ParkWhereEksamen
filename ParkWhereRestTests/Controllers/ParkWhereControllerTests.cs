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
            // 1. Setup af In-Memory Database options
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // 2. Initialiser Context og den rigtige GenericDbService
            _context = new MyDbContext(options);
            _carService = new GenericDbService<Car>(options);

            // 3. Setup af Mocks
            _mockParkingLot = new Mock<IParkingLot>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();

            // Fake HttpClient så den ikke er null
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                                  .Returns(new HttpClient());

            // 4. Seed databasen for at undgå API kald i controlleren
            _context.ParkingEvents.Add(new ParkingEvent { LicensePlate = "AB12345" });
            _context.SaveChanges();

            // 5. Setup af forventede svar fra ParkingLot (så dine tests passer)
            _mockParkingLot.Setup(x => x.EventTrigger(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                           .Returns(98);

            _mockParkingLot.Setup(x => x.GetAvailableSpaces())
                           .Returns(99);

            // 6. Instansier Controlleren med alle dependencies
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
            int expected = 98;

            ParkWhereController.PlateDto plateDto = new ParkWhereController.PlateDto
            {
                Plate = "AB12345",
                Time = DateTime.Now
            };

            Task<IActionResult> actionResult = _controller.ChangeParkingSpotAmount(plateDto);

            var result = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(result, "Resultatet var null - forventede OkObjectResult");

            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod()]
        public void GetParkingSpots()
        {
            int expected = 99;
            ActionResult<int> actual = _controller.GetAvailable();

            var actualResult = actual.Result as OkObjectResult;

            Assert.AreEqual(expected, actualResult.Value);
        }
    }
}