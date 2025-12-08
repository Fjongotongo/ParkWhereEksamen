using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Text.Json.Serialization;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        private readonly IParkingLot _parkingLot;

        // Constructor Injection
        public ParkWhereController(IParkingLot parkingLot)
        {
            _parkingLot = parkingLot;
        }

        public class PlateDto
        {
            public string Plate { get; set; }

            // Tells C# to look for "dateTime" in the JSON instead of "Time"
            [JsonPropertyName("dateTime")]
            public DateTime Time { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> ChangeParkingSpotAmount([FromBody] PlateDto plateDto)
        {
            // Use _parkingLotDb, NOT _parkingLot
            return Ok(_parkingLot.EventTrigger(plateDto.Plate, plateDto.Time, 1));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetAvailable()
        {
            // Use the DB service
            return Ok(_parkingLot.GetAvailableSpaces());
        }
    }
}