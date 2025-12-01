using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot
    {

        private List<ParkingLot> _parkingLots;

        private int _nextParkingLotId = 1;



        public int Id { get; set; }

        public string Name { get; set; }

        public int ParkingSpaces { get; set; }

        public int CarsParked { get; set; }

        public CarRepo CarRepo { get; set; }

        private List<ParkingEvent> _events = new List<ParkingEvent>();


        public ParkingLot() 
        {
            _parkingLots = new List<ParkingLot>()
            {
                new ParkingLot { Id = _nextParkingLotId++, Name = "P-Syd", ParkingSpaces = 75, CarsParked = 73 }
            };
        }

        public ParkingLot(string name, int parkingSpaces, int carsParked)
        {
            Name = name;
            ParkingSpaces = parkingSpaces;
            CarsParked = carsParked;
        }

        public override string ToString()
        {
            return $"Name {Name}, ParkingLots {ParkingSpaces}, CarsParked {CarsParked}";
        }

        public List<ParkingEvent> GetAllParkingEvents()
        {
            return null;
        }

        public ParkingEvent? GetById(int id)
        {
            return null;
        }

        public ParkingEvent AddParkingEvent(ParkingEvent parkingEvent)
        {
            return null;
        }

        public int CalculateParkingSpacesWhenCarIsDrivingIntoParkingLot(int parkingLotId)
        {
            ParkingLot? parkingToRemoveSpace = _parkingLots.FirstOrDefault(p => p.Id == parkingLotId);
            int parkingSpacesLeft = 0;

            if (parkingToRemoveSpace != null)
            {
                parkingToRemoveSpace.CarsParked += 1;
                parkingSpacesLeft = parkingToRemoveSpace.ParkingSpaces - parkingToRemoveSpace.CarsParked;
            }
            return parkingSpacesLeft;
        }

        public int CalculateParkingSpacesWhenCarIsDrivingOutOfParkingLot(int parkingLotId)
        {
            ParkingLot? parkingToAddSpace = _parkingLots.FirstOrDefault(p => p.Id == parkingLotId);
            int parkingSpacesLeft = 0;

            if (parkingToAddSpace != null)
            {
                parkingToAddSpace.CarsParked -= 1;
                parkingSpacesLeft = parkingToAddSpace.ParkingSpaces - parkingToAddSpace.CarsParked;
            }
            return parkingSpacesLeft;
        }

    }
}
