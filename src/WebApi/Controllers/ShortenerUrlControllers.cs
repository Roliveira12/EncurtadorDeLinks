using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerUrlControllers : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
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