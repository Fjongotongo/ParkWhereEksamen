using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkWhereLib;
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
        private CarRepo repo;

        [TestInitialize]

        public void Setup()
        {
            repo = new CarRepo();
        }

        [TestMethod()]
        public void GetAllCars_NoFilter_ReturnsAllCars()
        {
            var cars = repo.GetAllCars();
            Assert.AreEqual(3, cars.Count);
        }

        [TestMethod()]
        public void GetAllCars_FilterByBrand_ReturnsFilteredCars()
        {
            var cars = repo.GetAllCars(brand: "BMW");
            Assert.AreEqual(1, cars.Count);
            Assert.AreEqual("BMW", cars[0].Brand);
        }

        [TestMethod]
        public void GetAllCars_FilterByFuelType_ReturnsFilteredCars()
        {
            var cars = repo.GetAllCars(fuelType: "Petrol");
            Assert.AreEqual(2, cars.Count);

        }

        public void GetAllCars_FilterByModel_ReturnsFilteredCars()
        {
            var cars = repo.GetAllCars(model: "Punto");
            Assert.AreEqual(1, cars.Count);
            Assert.AreEqual("Punto", cars[0].Model);
        }

        [TestMethod()]
        public void GetCarById_ExistingId_ReturnsCar()
        {
            var car = repo.GetCarById(1);
            Assert.IsNotNull(car);
            Assert.AreEqual(1, car.Id);
        }

        [TestMethod]
        public void GetCarById_NonExisting_ReturnsNull()
        {
            var car = repo.GetCarById(4);
            Assert.IsNull(car);
        }

        [TestMethod]
        public void GetByLicensePlate_ExistingPlate_ReturnsCar()
        {
            var car = repo.GetByLicensePlate("XYZ789");
            Assert.IsNotNull(car);
            Assert.AreEqual("XYZ789", car.LicensePlate);
        }

        [TestMethod]
        public void GetByLicensePlate_NonExistingPlate_ReturnsNull()
        {
            var car = repo.GetByLicensePlate("NONEXISTENT");
            Assert.IsNull(car);
        }

        [TestMethod]
        public void AddCar_ValidCarCheckForLicensePlate()
        {
            var newCar = new Car
            {
                LicensePlate = "NEW123",
                Brand = "Audi",
                FuelType = "Diesel",
                Model = "A4"
            };
            repo.AddCar(newCar);
            repo.GetByLicensePlate("NEW123");
            Assert.AreEqual("NEW123", newCar.LicensePlate);
        }

        [TestMethod]
        public void AddCar_IfLicensePlateAlreadyExists()
        {
          var newCar = new Car
          {
              LicensePlate = "ABC123", // Existing license plate
              Brand = "Toyota",
              FuelType = "Hybrid",
              Model = "Corolla"
          };
            repo.AddCar(newCar);
            repo.GetByLicensePlate("ABC123");
           

        }

        [TestMethod]
        public void AddCar_DuplicateLicensePlate_DoesNotAddCar()
        {
            // Arrange
            var repo = new CarRepo();

            var car1 = new Car
            {
                LicensePlate = "ABC123",
                Brand = "BMW",
                FuelType = "Petrol",
                Model = "M3"
            };

            var car2 = new Car
            {
                LicensePlate = "ABC123",     // SAME plate
                Brand = "Audi",
                FuelType = "Diesel",
                Model = "A4"
            };

            // Act
            var firstAdd = repo.AddCar(car1);
            var secondAdd = repo.AddCar(car2);

            // Assert
            Assert.AreEqual(firstAdd.Id, secondAdd.Id, "Should return existing car instead of adding a new one.");

            var allCars = repo.GetAllCars();
            Assert.AreEqual(4, allCars.Count, "Repo should still contain only one car.");
        }


    }
}