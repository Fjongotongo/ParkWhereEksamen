using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot : IParkingLot
    {

        public int ParkingLotId { get; set; }

        public const int ParkingSpaces = 100;
        public int CarsParked { get; set; } = 0;

        public ParkingLot()
        {
        }

        private static int _nextEventId = 1;

        public ICollection<ParkingEvent> _events = new List<ParkingEvent>();

        public int StartParkingEvent(string licensePlate, DateTime entryTime)
        {
            ParkingEvent parkingEvent = new ParkingEvent(licensePlate, entryTime, 1);
            {
                parkingEvent.Id = _nextEventId++;
                parkingEvent.EntryTime = entryTime;
            }

            _events.Add(parkingEvent);

            CarsParked++;
            return ParkingSpaces - CarsParked;
        }

        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {
            ParkingEvent? evt = _events.FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);

            if (evt == null) return GetAvailableSpaces();

            evt.ExitTime = exitTime;

            CarsParked--;
            return ParkingSpaces - CarsParked;
        }

        public int EventTrigger(string licensePlate, DateTime time, int parkingLotId)
        {
            if (_events.Any(e => e.LicensePlate == licensePlate && e.ExitTime == null))
            {
                return EndParkingEvent(licensePlate, time);
            }
            return StartParkingEvent(licensePlate, time);
        }

        public int GetAvailableSpaces() => ParkingSpaces - CarsParked;

        // In ParkingLot.cs
        public List<int> GetAmountStartParkingEachHour()
        {
            // Return 24 zeros instead of null, so the chart simply shows flat lines
            return Enumerable.Repeat(0, 24).ToList();
        }
        // ParkingLot.cs
        public List<int> GetAmountStartParkingEachDay()
        {
            // Return 7 zeros (Mon-Sun) to satisfy the interface
            return Enumerable.Repeat(0, 7).ToList();
        }
    }
}
