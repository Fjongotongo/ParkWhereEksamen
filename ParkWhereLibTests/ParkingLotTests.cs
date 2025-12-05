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
                CarsParked = 1,
            };
        }

        [TestMethod()]
        public void Test_CarEnters()
        {
            int i = 98;
            int expected = _parkingLot.CarsEnters();
            Assert.AreEqual(expected, i);
        }

        [TestMethod()]
        public void Test_StartParkingEventReturnsUpdatedSpacesAfterNewParking()
        {
            string LicensePlate = "AB12345";
            DateTime EntryTime = DateTime.Now;
            int i = _parkingLot.StartParkingEvent(LicensePlate, EntryTime);
            ParkingEvent parkingevent = new ParkingEvent(LicensePlate, EntryTime, 1);
            Assert.AreEqual(_parkingLot._events.First().LicensePlate, parkingevent.LicensePlate);

            Assert.AreEqual(i, 98);
        }

        [TestMethod()]
        public void Test_CarExits()
        {
            int i = 100;
            int expected = _parkingLot.CarExits();
            Assert.AreEqual(expected, i);
        }

        [TestMethod()]
        public void Test_EndParkingEventWithEventFromList()
        {
            string licensePlate = "AB12345";
            DateTime entryTime = DateTime.Now.AddSeconds(-30);
            DateTime exitTime = DateTime.Now.AddSeconds(30);

            int i = _parkingLot.StartParkingEvent(licensePlate, entryTime);
            int end = _parkingLot.EndParkingEvent(licensePlate, exitTime) - 1;

            Assert.AreEqual(i, end);
        }

        [TestMethod()]
        public void Test_EndParkingEventWithNoEventFromList()
        {
            string licensePlate = "notvalid";
            DateTime entryTime = DateTime.Now.AddSeconds(-30);
            DateTime exitTime = DateTime.Now.AddSeconds(30);

            int expcected = _parkingLot.GetAvailableSpaces();
            int actual = _parkingLot.EndParkingEvent(licensePlate, exitTime);

            Assert.AreEqual(expcected, actual);
        }

        [TestMethod()]
        public void Test_EventTriggerEndsParkingEvent()
        {
            string licensePlate = "AB12345";
            DateTime entryTime = DateTime.Now.AddSeconds(-30);
            DateTime exitTime = DateTime.Now.AddSeconds(30);

            _parkingLot.EventTrigger(licensePlate, entryTime);

            int expected = 99;
            int actual = _parkingLot.EventTrigger(licensePlate, exitTime);


        }

        [TestMethod()]
        public void Test_EventTriggerStartsParkingEvent()
        {

            string licensePlate = "AB12345";
            DateTime entryTime = DateTime.Now.AddSeconds(-30);
            DateTime exitTime = DateTime.Now.AddSeconds(30);

            int expected = 98;
            int actual = _parkingLot.EventTrigger(licensePlate, entryTime);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_GetAvailableSpaces()
        {
            int expected = 99;
            int actual = _parkingLot.GetAvailableSpaces();
            Assert.AreEqual(expected, actual);
        }
    }
}