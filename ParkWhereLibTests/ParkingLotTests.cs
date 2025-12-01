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
    public class ParkingLotTests
    {
        private ParkingLot _parkingLot;

        [TestInitialize]
        public void TestInitialize()
        {
            //_parkingLot._events = new List<ParkingEvent>()
            //{
            //    new ParkingEvent(CarRepo.GetCarById(1), DateTime.Now),
            //    new ParkingEvent(CarRepo.GetCarById(2), DateTime.Now.AddMinutes(4)),
            //    new ParkingEvent(CarRepo.GetCarById(3), DateTime.Now)
            //};
        }

        [TestMethod]
        public void AddParkingEventTest()
        {
            Car car = new Car("Nummerplade", "Audi", "Benzin", "RS6");
            ParkingEvent parkingEvent = new ParkingEvent();
        }
    }
}