using ParkWhereLib.Models;
using System;
using System.Linq;

namespace ParkWhereLib
{
    public class ParkingLotDb
    {
        private readonly MyDbContext _context;

        public const int ParkingSpaces = 100;

        public ParkingLotDb(MyDbContext context)
        {
            _context = context;
        }


        // Logic to decide if we are starting or ending parking
        public int EventTrigger(string licensePlate, DateTime time, int parkingLotId)
        {
            // Check DB for an active parking event (ExitTime is null)
            var activeEvent = _context.ParkingEvents
                .FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);

            if (activeEvent != null)
            {
                return EndParkingEvent(activeEvent, time);
            }
            return StartParkingEvent(licensePlate, time);
        }

        public int StartParkingEvent(string licensePlate, DateTime entryTime)
        {

            int parkingLotId = 1; // assume only one lot

            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == parkingLotId);
            if (lot == null)
            {
                throw new Exception("Parking lot does not exist");
            }

            var parkingEvent = new ParkingEvent(licensePlate, entryTime, parkingLotId);
            _context.ParkingEvents.Add(parkingEvent);

            // increment CarsParked
            lot.CarsParked++;
            _context.ParkingLots.Update(lot);

            _context.SaveChanges();

            return ParkingLotDb.ParkingSpaces - lot.CarsParked;
        }

        public int EndParkingEvent(ParkingEvent evt, DateTime exitTime)
        {

            int parkingLotId = 1; // assume only one lot

            // 1. Get the parking lot
            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == parkingLotId);
            if (lot == null)
            {
                throw new Exception("Parking lot does not exist");
            }

            // 2. Set the exit time for the parking event
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
            // Calculate directly from DB: Total Spaces - Count of cars currently parked (ExitTime is null)
            int currentlyParked = _context.ParkingEvents.Count(e => e.ExitTime == null);
            return ParkingSpaces - currentlyParked;
        }
    }
}