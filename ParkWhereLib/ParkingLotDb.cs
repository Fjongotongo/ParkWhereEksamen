using ParkWhereLib.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace ParkWhereLib
{
    public class ParkingLotDb : IParkingLot
    {
        private readonly MyDbContext _context;

        public const int ParkingSpaces = 100;

        public ParkingLotDb(MyDbContext context)
        {
            _context = context;
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
            // Check DB for an active parking event (ExitTime is null)
            if (_context.ParkingEvents.Any(e => e.LicensePlate == licensePlate && e.ExitTime == null))
            {
                return EndParkingEvent(licensePlate, time);
            }
            return StartParkingEvent(licensePlate, time);
        }

        /// <summary>
        /// Method that created a new parking event when a car enters the parking lot.
        /// There is only 1 parking lot, therefore parkingLotId is always 1, but we still find it with MyDbContext.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="entryTime"></param>
        /// <returns>
        /// Returns the number of available parking spaces after the car has entered.
        /// </returns>
        public int StartParkingEvent(string licensePlate, DateTime entryTime)
        {

            int parkingLotId = 1; // assume only one lot

            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == parkingLotId);

            var parkingEvent = new ParkingEvent(licensePlate, entryTime, parkingLotId);
            _context.ParkingEvents.Add(parkingEvent);

            // increment CarsParked
            lot.CarsParked++;
            _context.ParkingLots.Update(lot);

            _context.SaveChanges();

            return ParkingLotDb.ParkingSpaces - lot.CarsParked;
        }


        /// <summary>
        /// End the active parking event for a parking event with matching license plate and exit time is null.
        /// There is only 1 parking lot, therefore parkingLotId is always 1, but we still find it with MyDbContext.
        /// Only makes CarsParked-- if there is space for a car, so it doesn't go negative.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="exitTime"></param>
        /// <returns>
        /// Returns the number of available parking spaces after the car has exited.
        /// </returns>
        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {

            int parkingLotId = 1; // assume only one lot

            // 1. Get the parking lot
            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == parkingLotId);

            // 2. Set the exit time for the parking event
            ParkingEvent? evt = _context.ParkingEvents.FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);

            if (evt == null) return GetAvailableSpaces();

            evt.ExitTime = exitTime;
            _context.ParkingEvents.Update(evt);

            // 3. Decrement the cars parked in the lot
            if (lot.CarsParked > 0)
            {
                lot.CarsParked--;
            }
            _context.ParkingLots.Update(lot);

            // 4. Save all changes to the database
            _context.SaveChanges();

            // 5. Return available spaces
            return ParkingLotDb.ParkingSpaces - lot.CarsParked;
        }

    
        public int GetAvailableSpaces()
        {
            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == 1);
            return ParkingLotDb.ParkingSpaces - lot.CarsParked;
        }

    }
}