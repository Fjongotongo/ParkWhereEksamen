using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

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

        private const string MotorApiKey = "mllr9po25fx4ylvtymlvwfoqxmxdh9rx";
        private const string MotorApiBase = "https://v1.motorapi.dk/";
        private IParkWhereRepo _repo;
        private HttpClient _httpClient;

        public ParkWhereController(IParkWhereRepo repo, HttpClient httpClient)
        {
            _repo = repo;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://v1.motorapi.dk");
            _httpClient.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "mllr9po25fx4ylvtymlvwfoqxmxdh9rx");
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Car>> GetAll()
        {
            return Ok(_repo.GetAllCars());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Get(int id)
        {
             Car? car = _repo.GetCarById(id);
             if (car == null)
             {
                return NotFound();
             }
             else
             {
                return Ok(car);
             }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            Car Car = _repo.AddCar(car);
            return Created("INSERT URL" + "/" + Car.Id, Car); //Mangler URL
        }
        
        [HttpPost("plate")]
        public async Task<IActionResult> ReceivePlate([FromBody] PlateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Plate))
                return BadRequest("No plate received");

            using var client = new HttpClient();
            client.BaseAddress = new System.Uri(MotorApiBase);
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", MotorApiKey);
            
            var response = await client.GetAsync($"vehicles?registration_number={dto.Plate}");
            var json = await response.Content.ReadAsStringAsync();

            return Content(json, "application/json"); // forward JSON directly
        }
    }
}
