using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkWhereRepo
    {
        private int _nextParkingLotId = 1;

        private List<ParkingLot> _parkingLots;

        public ParkWhereRepo()
        {

            _parkingLots = new List<ParkingLot>()
            {
                new ParkingLot { Id = _nextParkingLotId++, Name = "P-Syd", ParkingSpaces = 75, CarsParked = 73 }
            };
        }


        /// <summary>
        /// Calculates the amount of parking spaces left when a car has driven into the lot.
        /// </summary>
        /// <param name="parkingLotId"></param>
        /// <returns>
        /// Returns the amount of parkingspaces left after new car is parked.
        /// </returns>
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

        /// <summary>
        /// Calculates the amount of parking spaces left when a car has driven out of the lot
        /// </summary>
        /// <param name="parkingLotId"></param>
        /// <returns>
        /// Parking spaces left after car has left lot.
        /// </returns>
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
