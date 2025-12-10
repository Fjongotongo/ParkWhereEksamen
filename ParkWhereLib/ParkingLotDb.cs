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


        // Logic to decide if we are starting or ending parking
        public int EventTrigger(string licensePlate, DateTime time, int parkingLotId)
        {
            // Check DB for an active parking event (ExitTime is null)
            if (_context.ParkingEvents.Any(e => e.LicensePlate == licensePlate && e.ExitTime == null))
            {
                return EndParkingEvent(licensePlate, time);
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

        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {

            int parkingLotId = 1; // assume only one lot

            // 1. Get the parking lot
            var lot = _context.ParkingLots.FirstOrDefault(l => l.ParkingLotId == parkingLotId);
            if (lot == null)
            {
                throw new Exception("Parking lot does not exist");
            }

            // 2. Set the exit time for the parking event
            ParkingEvent? evt = _context.ParkingEvents.FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);
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


        public List<int> GetAmountStartParkingEachHour()
        {
            // Prepare a 24-item list (hours 0..23) initialized to 0
            var countsByHour = Enumerable.Repeat(0, 24).ToList();

            // 1. Calculate Start of Week (Monday)
            DateTime now = DateTime.Now;

            // This math ensures we go back to the most recent Monday (even if today is Sunday)
            int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = now.Date.AddDays(-1 * diff);

            // 2. Filter and Group
            var groups = _context.ParkingEvents
                // Optimization: Filter by date inside the database (before AsEnumerable)
                .Where(e => e.EntryTime >= startOfWeek)
                .AsEnumerable() // Switch to memory for the Hour grouping
                .GroupBy(e => e.EntryTime.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() });

            foreach (var g in groups)
            {
                if (g.Hour >= 0 && g.Hour < 24)
                {
                    countsByHour[g.Hour] = g.Count;
                }
            }

            return countsByHour;
        }

        public List<int> GetAmountStartParkingEachDay()
        {
            // Array for 7 days (0=Mon, 1=Tue ... 6=Sun)
            var countsByDay = new int[7];

            // Calculate Start of Week (Monday)
            DateTime now = DateTime.Now;
            int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = now.Date.AddDays(-1 * diff);

            var groups = _context.ParkingEvents
                .Where(e => e.EntryTime >= startOfWeek) // Filter for this week
                .AsEnumerable()
                .GroupBy(e => e.EntryTime.DayOfWeek)
                .Select(g => new { Day = g.Key, Count = g.Count() });

            foreach (var g in groups)
            {
                // Convert C# DayOfWeek (Sun=0, Mon=1) to Array Index (Mon=0, Sun=6)
                int index = g.Day == DayOfWeek.Sunday ? 6 : (int)g.Day - 1;
                countsByDay[index] = g.Count;
            }

            return countsByDay.ToList();
        }


    }
}