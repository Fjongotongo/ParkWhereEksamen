using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParkWhereLib
{
    public class Car
    {
        public int Id { get; set; }


        [JsonPropertyName("make")]
        public string Brand { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("fuel_type")]
        public string Fueltype { get; set; }


        public Car() { }


    }
}
