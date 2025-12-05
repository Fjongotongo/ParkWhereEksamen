using ParkWhereLib.DbService;
using ParkWhereLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLotDb
    {
        private MyDbContext _context;


        public int ParkingLotId { get; set; }
        public const int ParkingSpaces = 100;
        public int CarsParked { get; set; } = 1;

        public ParkingLotDb(MyDbContext context)
        {
            _context = context;
        }

        private static int _nextEventId = 1;

        public ICollection<ParkingEvent> _events = new List<ParkingEvent>();

        public int StartParkingEvent(string licensePlate, DateTime entryTime)
        {
            ParkingEvent parkingEvent = new ParkingEvent(licensePlate, entryTime);
            {
                parkingEvent.Id = _nextEventId++;
                parkingEvent.EntryTime = entryTime;
            }

            _context.ParkingEvents.Add(parkingEvent);
            _context.SaveChanges();
           // _events.Add(parkingEvent);

            return CarsEnters();
        }

        public int EndParkingEvent(string licensePlate, DateTime exitTime)
        {
            ParkingEvent? evt = _events.FirstOrDefault(e => e.LicensePlate == licensePlate && e.ExitTime == null);

            if (evt == null) return GetAvailableSpaces();

            evt.ExitTime = exitTime;
            _context.ParkingEvents.Update(evt);
            _context.SaveChanges();

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
           // _context.ParkingLots.Update(CarsParked);
            return ParkingSpaces - CarsParked;
        }

        public int CarExits()
        {
            CarsParked--;
            return ParkingSpaces - CarsParked;
        }

        public int GetAvailableSpaces() => ParkingSpaces - CarsParked;

    }
}
