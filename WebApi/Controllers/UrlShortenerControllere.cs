using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlShortenerControllere : ControllerBase
    {




        [HttpGet]
        public async Task<IActionResult> GetAllUrlAsync()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> GetByIdAsync(string urlId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("?{id}")]
        public async Task<IActionResult> CreateShortUrlAsync([FromQuery] string url)
        {
            return Ok();
        }
    }
}
