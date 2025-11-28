using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkWhereRepo : IParkWhereRepo
    {
        private int _nextId = 1;
        private int _nextParkingLotId = 1;

        private List<Car> _cars;
        private List<ParkingLot> _parkingLots;

        public ParkWhereRepo()
        {
            _cars = new List<Car>()
            {
                new Car {Id = _nextId++, LicensePlate = "ABC123", Entry = DateTime.Now.AddHours(-2), Exit = null, Brand = "BMW", FuelType = "Petrol", Model = "E39" },
                new Car {Id = _nextId++, LicensePlate = "XYZ789", Entry = DateTime.Now.AddHours(-1), Exit = null, Brand = "Fiat", FuelType = "Petrol", Model = "Punto"  },
                new Car {Id = _nextId++, LicensePlate = "LMN456", Entry = DateTime.Now.AddHours(-3), Exit = DateTime.Now.AddHours(-1), Brand = "Tesla", FuelType = "Electric", Model = "Y" }
            };

            _parkingLots = new List<ParkingLot>()
            {
                new ParkingLot { Id = _nextParkingLotId++, Name = "P-Syd", ParkingSpaces = 75, CarsParked = 73 }
            };
        }

        public List<Car> GetAllCars(string? brand = null, string? fuelType = null, string? model = null, string? sortBy = null)
        {

            var liste = new List<Car>(_cars);
            if (brand != null)
            {
                liste = liste.Where(c => c.Brand.ToLower().Contains(brand.ToLower())).ToList();
            }
            if (fuelType != null)
            {
                liste = liste.Where(c => c.FuelType.ToLower().Contains(fuelType.ToLower())).ToList();
            }
            if (model != null)
            {
                liste = liste.Where(c => c.Model.ToLower().Contains(model.ToLower())).ToList();
            }
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "brand":
                        liste = liste.OrderBy(c => c.Brand).ToList();
                        break;
                    case "brand_desc":
                        liste = liste.OrderByDescending(c => c.Brand).ToList();
                        break;
                    case "fueltype":
                        liste = liste.OrderBy(c => c.FuelType).ToList();
                        break;
                    case "fueltype_desc":
                        liste = liste.OrderByDescending(c => c.FuelType).ToList();
                        break;
                    case "model":
                        liste = liste.OrderBy(c => c.Model).ToList();
                        break;
                    case "model_desc":
                        liste = liste.OrderByDescending(c => c.Model).ToList();
                        break;
                }
            }
            return liste;
        }

        public Car? GetCarById(int id)
        {
            return _cars.FirstOrDefault(c => c.Id == id);
        }

        public Car AddCar(Car car)
        {
            car.Id = _nextId++;
            _cars.Add(car);
            return car;
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
