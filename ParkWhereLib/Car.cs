namespace ParkWhereLib
{
    public class Car
    {
        public int Id { get; set; }

        public string? LicensePlate { get; set; }

        public DateTime Entry { get; set; }

        public DateTime? Exit { get; set; }

        public string Brand { get; set; }

        public string FuelType { get; set; }

        public string Model { get; set; }

        public Car() { }

        public Car(string licensePlate, DateTime entry, DateTime? exit, string brand, string fuelType, string model)
        {
            LicensePlate = licensePlate;
            Entry = entry;
            Exit = exit;
            Brand = brand;
            FuelType = fuelType;
            Model = model;
        }

        public override string ToString()
        {
            return $"LicensePlate {LicensePlate}, Entry {Entry}, Exit {Exit}, Brand {Brand}, FuelType {FuelType}, Model {Model}";
        }

    }
}
