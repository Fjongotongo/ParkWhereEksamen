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
        private CarRepo _carRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            _carRepo = new CarRepo();
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
    }
}