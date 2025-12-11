using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkWhereLib;
using ParkWhereLib.DbService;
using ParkWhereLib.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        private readonly IParkingLot _parkingLot;
        private readonly HttpClient _httpClient;
        private readonly GenericDbService<Car> _carService;

        private readonly MyDbContext _context;

        public ParkWhereController(IParkingLot parkingLot,
                                   IHttpClientFactory httpClientFactory,
                                   GenericDbService<Car> carService,
                                   MyDbContext context)
        {
            _parkingLot = parkingLot;
            _httpClient = httpClientFactory.CreateClient("MotorApi");
            _carService = carService;
            _context = context;
        }

        public class PlateDto
        {
            public string Plate { get; set; }

            // Tells C# to look for "dateTime" in the JSON instead of "Time"
            [JsonPropertyName("dateTime")]
            public DateTime Time { get; set; }
        }
        public class CarDto
        {
            public int TotalCars { get; set; }
            public Dictionary<string, BrandDto> Brands { get; set; }
            public Dictionary<string, int> CarsByFueltype { get; set; }
        }

        public class BrandDto
        {
            public int Count { get; set; }
            public Dictionary<string, int> Models { get; set; }
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeParkingSpotAmount([FromBody] PlateDto dto)
        {


            var existingEvent = await _context.ParkingEvents
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(e => e.LicensePlate == dto.Plate);


            if (existingEvent == null)
            {
                var response = await _httpClient.GetAsync($"vehicles?registration_number={dto.Plate}");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var carsFromApi = JsonSerializer.Deserialize<Car[]>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var car = carsFromApi?.FirstOrDefault();
                if (car != null)
                {
                    await _carService.AddObjectAsync(car); // Save locally
                }
                
            }

            // Trigger parking lot event
            return Ok(_parkingLot.EventTrigger(dto.Plate, dto.Time));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("stats")]
        public async Task<ActionResult<CarDto>> GetCarStatistics()
        {
            var cars = await _carService.GetObjectsAsync();

            var brands = cars
                .GroupBy(c => c.Brand)
                .ToDictionary(
                    g => g.Key,
                    g => new BrandDto
                    {
                        Count = g.Count(),
                        Models = g.GroupBy(c => c.Model)
                                  .ToDictionary(mg => mg.Key, mg => mg.Count())
                    });

            var stats = new CarDto
            {
                TotalCars = cars.Count(),
                Brands = brands,
                CarsByFueltype = cars.GroupBy(c => c.Fueltype)
                                     .ToDictionary(fg => fg.Key, fg => fg.Count())
            };

            return Ok(stats);
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