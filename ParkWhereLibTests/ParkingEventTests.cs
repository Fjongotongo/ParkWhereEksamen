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
    public class ParkingEventTests
    {
        private ParkingEvent _parkingEvent;

        [TestMethod]
        public void Test_ShouldReturnNull_WhenExitTimeIsNull()
        {

            var parkingEvent = new ParkingEvent
            {

            };

            var exitTime = parkingEvent.ExitTime;

            Assert.IsNull(exitTime);
        }

        [TestMethod]
        public void Test_ShouldSetExitTime_WhenExitTimeIsProvided()
        {

            var parkingEvent = new ParkingEvent
            {

            };

            var expectedExitTime = DateTime.Now;
            parkingEvent.ExitTime = expectedExitTime;
            var actualExitTime = parkingEvent.ExitTime;
            Assert.AreEqual(expectedExitTime, actualExitTime);
        }

        [TestMethod]
        public void Test_ShouldReturnLicensePlate_WhenLicensePlateIsSet()
        {

            var parkingEvent = new ParkingEvent
            {
                LicensePlate = "ABC123"
            };

            var licensePlate = parkingEvent.LicensePlate;
            Assert.AreEqual("ABC123", licensePlate);
        }

    }
    
    
}