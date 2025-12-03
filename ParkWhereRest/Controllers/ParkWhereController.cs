using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Text.Json;

namespace ParkWhereRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkWhereController : ControllerBase
    {
        // Denne klasse matcher JSON fra din Raspberry Pi
        public class PlateDto
        {
            public string Plate { get; set; }
        }

    }
}