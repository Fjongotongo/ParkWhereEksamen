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

        // Add this to satisfy the database
        public int ParkingLotId { get; set; }

        // Constructor fix
        public ParkingEvent(string licensePlate, DateTime entryTime)
        {
            LicensePlate = licensePlate;
            EntryTime = entryTime;
            ParkingLotId = 1; // You must default this to a valid ID (e.g., 1)
        }

        public ParkingEvent() { }
    }
}
