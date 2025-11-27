using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot
    {
        private int _parkingSpaces;

        public int Id { get; set; }

        public string Name { get; set; }

        public int ParkingSpaces
        {
            get => _parkingSpaces;

            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("Parking spaces must be greater than zero");
                if (value > 75) throw new ArgumentOutOfRangeException("Parking spaces must be 75 or less");

                _parkingSpaces = value;
            }
        }

        public ParkingLot() 
        {
         
        }

        public ParkingLot(string name, int parkingSpaces)
        {
            Name = name;
            ParkingSpaces = parkingSpaces;
        }

        public override string ToString()
        {
            return $"Name {Name}, ParkingLots {ParkingSpaces}";
        }

    }
}
