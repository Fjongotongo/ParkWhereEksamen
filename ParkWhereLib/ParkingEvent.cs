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

        public Car Car { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public ParkingEvent(Car car)
        {
            Car = car; 
            EntryTime = DateTime.Now;
        }

        public ParkingEvent() { }
    }
}
