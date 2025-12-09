using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkWhereLib;
using ParkWhereLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib.Tests
{
    [TestClass()]
    public class CarRepoTests
    {
        private CarRepo _carRepo;
        private MyDbContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            _carRepo = new CarRepo();

            var options = new DbContextOptionsBuilder<MyDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            _context = new MyDbContext(options);

            // Seed some cars
            _context.Cars.AddRange(
                new Car { Brand = "Toyota", Model = "Corolla", Fueltype = "Gas" },
                new Car { Brand = "Honda", Model = "Civic", Fueltype = "Diesel" }
            );
            _context.SaveChanges();
        }

        [TestMethod()]
        public void CreateCarTest()
        {
            Car car = new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Fueltype = "Gas"
            };

            int expectedId = 1;

            Car carAdded = _carRepo.AddCarToRepo(car);

            Assert.AreEqual(expectedId, carAdded.Id);
            Assert.AreEqual(car.Brand, carAdded.Brand);
            Assert.AreEqual(car.Model, carAdded.Model);
            Assert.AreEqual(car.Fueltype, carAdded.Fueltype);
        }

        [TestMethod()]
        public void AddCarToRepoTest()
        {
            Car car = new Car
            {
                Brand = "Honda",
                Model = "Civic",
                Fueltype = "Diesel"
            };
            Car carAdded = _carRepo.AddCarToRepo(car);
            Assert.IsNotNull(carAdded);
            Assert.AreEqual(1, carAdded.Id);
            Assert.AreEqual("Honda", carAdded.Brand);
            Assert.AreEqual("Civic", carAdded.Model);
            Assert.AreEqual("Diesel", carAdded.Fueltype);
        }   

        [TestMethod()] 
         public void GetCarsTest()
        {
            Car car1 = new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Fueltype = "Gas"
            };
            Car car2 = new Car
            {
                Brand = "Honda",
                Model = "Civic",
                Fueltype = "Diesel"
            };
            _carRepo.AddCarToRepo(car1);
            _carRepo.AddCarToRepo(car2);
            List<Car> cars = _carRepo.GetCarsFromRepo();
            Assert.AreEqual(2, cars.Count);
            Assert.AreEqual("Toyota", cars[0].Brand);
            Assert.AreEqual("Honda", cars[1].Brand);
        }

        public async Task GetCarsFromDbTest()
        {
            // Act
            var cars = await _context.Cars.ToListAsync();

            // Assert
            Assert.AreEqual(2, cars.Count);

            Assert.AreEqual("Toyota", cars[0].Brand);
            Assert.AreEqual("Corolla", cars[0].Model);

            Assert.AreEqual("Honda", cars[1].Brand);
            Assert.AreEqual("Civic", cars[1].Model);
        }

        [TestMethod]
        public async Task AddCarToDbTest()
        {
            // Arrange
            var newCar = new Car { Brand = "Ford", Model = "Focus", Fueltype = "Gas" };

            // Act
            _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();

            var cars = await _context.Cars.ToListAsync();

            // Assert
            Assert.AreEqual(3, cars.Count);
            Assert.IsTrue(cars.Any(c => c.Brand == "Ford" && c.Model == "Focus"));
        }
    }
}