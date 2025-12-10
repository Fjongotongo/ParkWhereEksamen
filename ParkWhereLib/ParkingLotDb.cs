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
            int currentlyParked = _context.ParkingEvents.Count(e => e.ExitTime == null);
            return ParkingSpaces - currentlyParked;
        }


        public List<int> GetAmountStartParkingEachHour()
        {
            return GetWeeklyStats(24, date => date.Hour);
        }

        public List<int> GetAmountStartParkingEachDay()
        {
            
            return GetWeeklyStats(7, date => ((int)date.DayOfWeek + 6) % 7);
        }

        private List<int> GetWeeklyStats(int totalSlots, Func<DateTime, int> selector)
        {
            var startOfWeek = DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek + 6) % 7);

            int[] finalCounts = new int[totalSlots];

            
            var counts = _context.ParkingEvents
                .Where(e => e.EntryTime >= startOfWeek)
                .Select(e => e.EntryTime)
                .AsEnumerable();

            foreach (var date in counts)
            {
                int index = selector(date);

                if (index >= 0 && index < totalSlots)
                {
                    finalCounts[index]++;
                }
            }

            return finalCounts.ToList();
        }


    }
}