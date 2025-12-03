using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot
    {


        public int Id { get; set; }
        public int ParkingSpaces { get; set; }
        public int CarsParked { get; set; }
        public int AvailableSpaces { get; set; }

        public ParkingLot()
        {
        }

        private static int _nextEventId = 1;

        public List<ParkingEvent> _events = new List<ParkingEvent>();

        public int StartParkingEvent(string licensePlate, DateTime entryTime)
        {
            ParkingEvent parkingEvent = new ParkingEvent(licensePlate, entryTime);
            {
                Id = _nextEventId++;
            }

            _events.Add(parkingEvent);

            return CarsEnters();
        }

        public int CarsEnters()
        {
            CarsParked++;
            return AvailableSpaces = ParkingSpaces - CarsParked;
        }
        

    }
}
