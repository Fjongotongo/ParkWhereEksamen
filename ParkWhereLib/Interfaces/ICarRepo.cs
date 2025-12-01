namespace ParkWhereLib.Interfaces
{
    public interface ICarRepo
    {
        Car AddCar(Car car);
        List<Car> GetAllCars(string? brand = null, string? fuelType = null, string? model = null, string? sortBy = null);
        Car? GetCarById(int id);
    }
}