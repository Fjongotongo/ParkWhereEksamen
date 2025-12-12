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


        /// <summary>
        /// Method that created a new parking event when a car enters the parking lot.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="entryTime"></param>
        /// <returns>
        /// Returns the number of available parking spaces after the car has entered.
        /// </returns>
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


        /// <summary>
        /// End the active parking event for a parking event with matching license plate and exit time is null.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="exitTime"></param>
        /// <returns>
        /// Returns the number of available parking spaces after the car has exited.
        /// </returns>
        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {
            ParkingEvent? evt = _events.FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);

            if (evt == null) return GetAvailableSpaces();

            evt.ExitTime = exitTime;

            CarsParked--;
            return ParkingSpaces - CarsParked;
        }


        /// <summary>
        /// Method that triggers either start or end of a parking event based on the license plate and if exit time is null.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="time"></param>
        /// <returns>
        /// Returns the number of available parking spaces after the event has been triggered.
        /// </returns>
        public int EventTrigger(string licensePlate, DateTime time)
        {
            if (_events.Any(e => e.LicensePlate == licensePlate && e.ExitTime == null))
            {
                return EndParkingEvent(licensePlate, time);
            }
            return StartParkingEvent(licensePlate, time);
        }

        public int GetAvailableSpaces() => ParkingSpaces - CarsParked;
    }
}
