using ParkWhereLib;

Car car = new Car
{
    LicensePlate = "AB12345",
    Brand = "Toyota",
    Model = "Corolla",
    FuelType = "Gasoline"
};

Console.WriteLine(car.ToString());

CarRepo repo = new CarRepo();

repo.AddCar(car);


Console.WriteLine(repo.ToString());