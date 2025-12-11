using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkWhereLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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

            ParkingEvent evt = new ParkingEvent
            {
                LicensePlate = "AB12345",
                EntryTime = DateTime.Now.AddMinutes(-30),
                ExitTime = null,
                ParkingLotId = 1
            };
            _parkingLot.EventTrigger("AB12345", DateTime.Now.AddMinutes(-30));
            _parkingLot.EventTrigger("AB12345", DateTime.Now.AddMinutes(-30));
            _parkingLot.EventTrigger("AB12346", DateTime.Now.AddMinutes(-30));
        }


        [TestMethod()]
        public void Test_StartParkingEventReturnsUpdatedSpacesAfterNewParking()
        {
            string LicensePlate = "AB12345";
            DateTime EntryTime = DateTime.Now;
            int i = _parkingLot.StartParkingEvent(LicensePlate, EntryTime);
            ParkingEvent parkingevent = new ParkingEvent(LicensePlate, EntryTime, 1);
            Assert.AreEqual(_parkingLot._events.First().LicensePlate, parkingevent.LicensePlate);

            Assert.AreEqual(i, 97);
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

            int expected = 97;
            int actual = _parkingLot.EventTrigger(licensePlate, entryTime);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Test_GetAvailableSpaces()
        {
            int expected = 98;
            int actual = _parkingLot.GetAvailableSpaces();
            Assert.AreEqual(expected, actual);
        }

    }
}