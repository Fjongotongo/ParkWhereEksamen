using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingEvent
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public ParkingEvent(string licensePlate, DateTime dateTime)
        {
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
        }

        public ParkingEvent() { }
    }
}
