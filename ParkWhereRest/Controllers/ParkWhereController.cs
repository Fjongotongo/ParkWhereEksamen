using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Text.Json;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        private ParkingLot _parkingLot;
        public ParkWhereController(ParkingLot parkingLot)
        {
            _parkingLot = parkingLot;
        }

        public class PlateDto
        {
            public string Plate { get; set; }
            public DateTime time { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int > Available_Parking_Spots([FromBody] PlateDto plateDto)
        {
            return Ok(_parkingLot.EventTrigger(plateDto.Plate, plateDto.time));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetAvailable()
        {
            return Ok(_parkingLot.GetAvailableSpaces());
        }

    }
}