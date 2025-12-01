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
        private CarRepo _carRepo;

        [TestInitialize]
        public void Setup()
        {
            _parkingEvent = new ParkingEvent();
            _carRepo = new CarRepo();
        }

        [TestMethod()]
        public void CreateCarIfLicensePlateExists()
        {



        }

        [TestMethod()]
        public void ParkingEventTest1()
        {
        }
    }
}