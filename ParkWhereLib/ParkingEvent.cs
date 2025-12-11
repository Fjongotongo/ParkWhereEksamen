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

        public int ParkingLotId { get; set; }

        public ParkingEvent(string licensePlate, DateTime entryTime, int parkingLotId)
        {
            LicensePlate = licensePlate;
            EntryTime = entryTime;
            ParkingLotId = parkingLotId;
        }

        public ParkingEvent() { }
    }
}
