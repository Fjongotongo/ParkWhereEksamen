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

        [TestMethod()]
        public void AddCar_ValidCar_AddsCar()
        {
            var newCar = new Car
            {
                LicensePlate = "NEW123",
                Brand = "Audi",
                FuelType = "Diesel",
                Model = "A4"
            };
            repo.AddCar(newCar);
            var car = repo.GetCarById(4);
            Assert.IsNotNull(car);
            Assert.AreEqual("NEW123", car.LicensePlate);
        }
    }
}