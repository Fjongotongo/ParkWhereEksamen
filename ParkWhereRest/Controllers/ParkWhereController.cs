using Microsoft.AspNetCore.Mvc;
using ParkWhereLib;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParkWhereLib.Interfaces;

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

        private readonly ICarRepo _repo;
        private readonly HttpClient _httpClient;

        public ParkWhereController(ICarRepo carRepo, IHttpClientFactory httpClientFactory)
        {
            _repo = carRepo;
            _httpClient = httpClientFactory.CreateClient("MotorApi"); //  Retrieve the named client
        }



        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<int> GetAvailableSpaces(int lotId)
        //{

        //}


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

                return Content(json, "application/json");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    $"Error contacting Motor API: {ex.Message}");
            }
        }

    }
}
