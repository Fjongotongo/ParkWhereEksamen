using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;

namespace ParkWhereRest.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {

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
    }
}
