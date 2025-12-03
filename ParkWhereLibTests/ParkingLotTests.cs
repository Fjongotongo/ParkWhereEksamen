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
       
        public void Setup()
        {
            _parkingLot = new ParkingLot
            {
                ParkingSpaces = 100,
                CarsParked = 0,
                AvailableSpaces = 100
            };
        }

        [TestMethod()]
        public void Test_CarEnters()
        {
            int i = 99;
            int expected = _parkingLot.CarsEnters();
            Assert.AreEqual(expected, i);
        }

        [TestMethod()]
        public void Test_StartParkingEvent()
        {
            string LicensePlate = "AB12345";
            DateTime EntryTime = DateTime.Now;
            int i = _parkingLot.StartParkingEvent(LicensePlate, EntryTime);
            ParkingEvent parkingevent = new ParkingEvent(LicensePlate, EntryTime);
            Assert.AreEqual(_parkingLot._events[0].LicensePlate, parkingevent.LicensePlate);
            Assert.AreEqual(i, 99);
        }


    }
}