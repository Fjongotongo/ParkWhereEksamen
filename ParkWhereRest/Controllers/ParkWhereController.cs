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
