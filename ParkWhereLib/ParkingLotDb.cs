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
        public int EventTrigger(string licensePlate, DateTime time)
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
            ParkingEvent parkingEvent = new ParkingEvent(licensePlate, entryTime);

            // You MUST link it to the existing lot ID
            parkingEvent.ParkingLotId = 1;

            _context.ParkingEvents.Add(parkingEvent);
            _context.SaveChanges();

            return GetAvailableSpaces();
        }

        public int EndParkingEvent(ParkingEvent evt, DateTime exitTime)
        {
            evt.ExitTime = exitTime;

            _context.ParkingEvents.Update(evt);
            _context.SaveChanges();

            return GetAvailableSpaces();
        }

        public int GetAvailableSpaces()
        {
            // Calculate directly from DB: Total Spaces - Count of cars currently parked (ExitTime is null)
            int currentlyParked = _context.ParkingEvents.Count(e => e.ExitTime == null);
            return ParkingSpaces - currentlyParked;
        }
    }
}