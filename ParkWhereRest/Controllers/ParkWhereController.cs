using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        private ParkingLot _parkingLot;
        private readonly HttpClient _httpClient;


        public ParkWhereController(ParkingLot parkingLot, IHttpClientFactory httpClientFactory)
        {
            _parkingLot = parkingLot;
            _httpClient = httpClientFactory.CreateClient("MotorApi"); //  Retrieve the named client

        }

        public class PlateDto
        {
            public string Plate { get; set; }
            public DateTime Time { get; set; }
        }

        public class CarDto
        {
            [JsonPropertyName("make")]
            public string Brand { get; set; }

            [JsonPropertyName("model")]
            public string Model { get; set; }

            [JsonPropertyName("fuel_type")]
            public string Fueltype { get; set; }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int > Available_Parking_Spots([FromBody] PlateDto plateDto)
        {
            return Ok(_parkingLot.EventTrigger(plateDto.Plate, plateDto.Time));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetAvailable()
        {
            return Ok(_parkingLot.GetAvailableSpaces());
        }

        // POST: api/parkwhere/plate

        [HttpPost("plate")]
        public async Task<IActionResult> ReceivePlate([FromBody] PlateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Plate))
                return BadRequest("No plate received");

            try
            {

                // Use the configured HttpClient directly
                var response = await _httpClient.GetAsync($"vehicles?registration_number={dto.Plate}");
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)  
                    return StatusCode((int)response.StatusCode, json);

                Console.WriteLine(json);

                // Deserialize JSON into CarDto
                var cars = JsonSerializer.Deserialize<List<CarDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });



                if (cars == null || cars.Count == 0)
                    return NotFound("No vehicle data found");

                var car = cars[0];

                return Ok(car);
            }


            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    $"Error contacting Motor API: {ex.Message}");
            }
        }



    }
}