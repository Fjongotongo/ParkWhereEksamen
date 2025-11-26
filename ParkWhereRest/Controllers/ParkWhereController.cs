using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;

namespace ParkWhereRest.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {

        private ParkWhereRepo _repo;

    }
}
