using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParkingSpaces { get; set; }

        public int CarsParked { get; set; }

        public CarRepo CarRepo { get; set; }

        private List<ParkingEvent> _events = new List<ParkingEvent>();

        public ParkingLot() 
        {
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

    }
}
