using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class CarRepo
    {
        private int _nextId = 1;

        private List<Car> _cars;

        public CarRepo()
        {
            _cars = new List<Car>()
            {
                new Car {Id = _nextId++, LicensePlate = "ABC123", Brand = "BMW", FuelType = "Petrol", Model = "E39" },
                new Car {Id = _nextId++, LicensePlate = "XYZ789", Brand = "Fiat", FuelType = "Petrol", Model = "Punto"  },
                new Car {Id = _nextId++, LicensePlate = "LMN456", Brand = "Tesla", FuelType = "Electric", Model = "Y" }
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
            //skal laves om så den tjekker om den allerede eksistere og ikke adder igen hvis den findes
            car.Id = _nextId++;
            _cars.Add(car);
            return car;
        }
    }
}
