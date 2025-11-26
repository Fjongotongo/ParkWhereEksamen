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

        public int ParkingLots { get; set; }

        public ParkingLot() 
        {
         
        }

        public ParkingLot(string name, int parkingLots)
        {
            Name = name;
            ParkingLots = parkingLots;
        }

        public override string ToString()
        {
            return $"Name {Name}, ParkingLots {ParkingLots}";
        }

    }
}
