using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib.Services
{
    public class ParkingLotService
    {
        private readonly List<ParkingLot> _lots = new();

        public ParkingLotService()
        {
            _lots.Add(new ParkingLot
            {
                Id = 1,
                Name = "P-Syd",
                ParkingSpaces = 75,
                CarsParked = 0,
                AvailableSpaces = 75
            });
        }

        public ParkingLot? GetById(int id)
            => _lots.FirstOrDefault(l => l.Id == id);

        public int CarEnters(int lotId)
        {
            var lot = GetById(lotId);
            if (lot == null) return -1;

            lot.CarsParked++;
            return lot.AvailableSpaces = lot.ParkingSpaces - lot.CarsParked;
        }

        public int CarExits(int lotId)
        {
            var lot = GetById(lotId);
            if (lot == null) return -1;

            lot.CarsParked--;
            return lot.AvailableSpaces = lot.ParkingSpaces - lot.CarsParked;
        }
    }

}
