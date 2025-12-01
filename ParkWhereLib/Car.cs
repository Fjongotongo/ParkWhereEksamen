namespace ParkWhereLib
{
    public class Car
    {
        public int Id { get; set; }

        public string? LicensePlate { get; set; }

        public string Brand { get; set; }

        public string FuelType { get; set; }

        public string Model { get; set; }

        public Car() { }

        public Car(string licensePlate, string brand, string fuelType, string model)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            FuelType = fuelType;
            Model = model;
        }

        public override string ToString()
        {
            return $"LicensePlate {LicensePlate}, Brand {Brand}, FuelType {FuelType}, Model {Model}";
        }

    }
}
