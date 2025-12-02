using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib.Services
{
    public class ParkingEventService
    {
        private readonly List<ParkingEvent> _events = new List<ParkingEvent>();
        private int _nextId = 1;

        private readonly ParkingLotService _lotService;
        private CarRepo _carRepo;

        public ParkingEventService(ParkingLotService lotService)
        {
            _lotService = lotService;
        }

        public ParkingEvent StartParkingEvent(int lotId, Car car)
        {
            // Create event
            var parkingEvent = new ParkingEvent(car)
            {
                Id = _nextId++
            };

            _events.Add(parkingEvent);
            _carRepo.AddCar(car);

            // Update lot status
            _lotService.CarEnters(lotId);

            return parkingEvent;
        }

        public ParkingEvent? EndParkingEvent(int parkingEventId)
        {
            var evt = _events.FirstOrDefault(e => e.Id == parkingEventId);

            if (evt == null || evt.ExitTime != null)
                return null;

            evt.ExitTime = DateTime.Now;

            // Update lot status

            return evt;
        }

        public ParkingEvent? GetById(int id) => _events.FirstOrDefault(e => e.Id == id);

        public List<ParkingEvent> GetAll() => _events.ToList();
    }

}
