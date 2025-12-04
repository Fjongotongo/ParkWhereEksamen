using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class CarRepo
    {
        public List<Car> Cars = new List<Car>();

        private int _nextId = 1;

        public Car AddCarToRepo(Car car)
        {
            car.Id = _nextId++;
            Cars.Add(car);

            return car;
        }
    }
}
