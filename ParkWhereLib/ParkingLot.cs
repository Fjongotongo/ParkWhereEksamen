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
        public int ParkingSpaces { get; set; } = 75;
        public int CarsParked { get; set; } = 1;
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

        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {
            ParkingEvent? evt = _events.FirstOrDefault(e => e.LicensePlate ==  licensePlate && e.ExitTime == null);

            if (evt == null) return AvailableSpaces;

            evt.ExitTime = exitTime;

            return CarExits();
        }

        public int EventTrigger(string licensePlate, DateTime time)
        {
            if (_events.Any(e => e.LicensePlate == licensePlate && e.ExitTime == null))
            {
                return EndParkingEvent(licensePlate, time);
            }
            return StartParkingEvent(licensePlate, time);
        }

        public int CarsEnters()
        {
            CarsParked++;
            return AvailableSpaces = ParkingSpaces - CarsParked;
        }

        public int CarExits()
        {
            CarsParked--;
            return AvailableSpaces = ParkingSpaces - CarsParked;
        }

        public int GetAvailableSpaces() => AvailableSpaces = ParkingSpaces - CarsParked;
        

    }
}
