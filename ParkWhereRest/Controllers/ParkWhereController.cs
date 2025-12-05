using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Text.Json.Serialization;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        private readonly ParkingLotDb _parkingLotDb;

        // Constructor Injection
        public ParkWhereController(ParkingLotDb parkingLotDb)
        {
            _parkingLotDb = parkingLotDb;
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
            return Ok(_parkingLotDb.EventTrigger(plateDto.Plate, plateDto.Time, 1));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetAvailable()
        {
            // Use the DB service
            return Ok(_parkingLotDb.GetAvailableSpaces());
        }
    }
}