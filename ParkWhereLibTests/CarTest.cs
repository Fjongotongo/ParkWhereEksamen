using ParkWhereLib;

namespace ParkWhereLibTests;

[TestClass()]
public class CarRepoTests
{
    private CarRepo repo;

    [TestInitialize]

    public void Setup()
    {
        repo = new CarRepo();
    }

    public void AddCarIfAlreadyExists()
    {
        var existingCar = repo.GetAllCars().First();

        var newCar = new Car
        {
            LicensePlate = existingCar.LicensePlate,
            Brand = existingCar.Brand,
            FuelType = existingCar.FuelType,
            Model = existingCar.Model
        };

        repo.AddCar(newCar);
        var cars = repo.GetAllCars().Where(c => c.LicensePlate == existingCar.LicensePlate).ToList();

        Assert.AreEqual(1, cars.Count); // Ensure no duplicate was added
    }

    public void AddCarIfNotExists()
    {
        var newCar = new Car
        {
            LicensePlate = "NEW123",
            Brand = "Audi",
            FuelType = "Diesel",
            Model = "A4"
        };
        // Assuming there's an AddCar method in CarRepo
        repo.AddCar(newCar);
        var car = repo.GetAllCars().FirstOrDefault(c => c.LicensePlate == "NEW123");
        Assert.IsNotNull(car); // Ensure the new car was added
        Assert.AreEqual("Audi", car.Brand);
    }
}
