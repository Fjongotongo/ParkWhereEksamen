using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class ParkingLot
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public int ParkingSpaces { get; set; }
        public int CarsParked { get; set; }

        private List<ParkingEvent> _events = new List<ParkingEvent>();

        public IReadOnlyList<ParkingEvent> Events => _events;

        public void AddEvent(ParkingEvent e)
        {
            _events.Add(e);
        }

    }
}
