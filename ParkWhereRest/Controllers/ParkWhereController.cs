using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        public class PlateDto
        {
            public string Plate { get; set; }
        }

        private readonly IParkWhereRepo _repo;
        private readonly HttpClient _httpClient;

        public ParkWhereController(IParkWhereRepo repo, IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClient = httpClientFactory.CreateClient("MotorApi"); //  Retrieve the named client
        }

        // GET: api/parkwhere
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Car>> GetAll()
        {
            return Ok(_repo.GetAllCars());
        }

        // GET: api/parkwhere/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Get(int id)
        {
            var car = _repo.GetCarById(id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // POST: api/parkwhere
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            var createdCar = _repo.AddCar(car);
            // Automatically generate the URL using Get action
            return CreatedAtAction(nameof(Get), new { id = createdCar.Id }, createdCar);
        }

        // POST: api/parkwhere/plate
        
        [HttpPost("plate")]
        public async Task<IActionResult> ReceivePlate([FromBody] PlateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Plate))
                return BadRequest("No plate received");
            // Debug: Log the BaseAddress
            Console.WriteLine($"HttpClient BaseAddress: {_httpClient.BaseAddress}");


            try
            {
                // Now this works because BaseAddress is set
                var response = await _httpClient.GetAsync($"vehicles?registration_number={dto.Plate}");
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, json);

                return Content(json, "application/json");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Error contacting Motor API: {ex.Message}");
            }
        }
    }
}
