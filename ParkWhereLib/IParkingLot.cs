
namespace ParkWhereLib
{
    public interface IParkingLot
    {
        int EndParkingEvent(string licensePlate, DateTime exitTime);
        int EventTrigger(string licensePlate, DateTime time);
        int GetAvailableSpaces();
        int StartParkingEvent(string licensePlate, DateTime entryTime);
    }
}