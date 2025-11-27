using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public interface IParkWhereRepo
    {
        List<Car> GetAllCars(string? brand = null, string? fuelType = null, string? model = null, string? sortBy = null);
        Car? GetCarById(int id);
        Car AddCar(Car car);
    }
}
