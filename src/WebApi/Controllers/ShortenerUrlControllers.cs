using Microsoft.AspNetCore.Mvc;
using WebApi.Boundaries.ShortenerUrl;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerUrlControllers : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShortenerUrlOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShortenerUrlOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(string urlId)
        {
            return Ok();
        }

        [HttpPost("?{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateShortUrlAsync([FromQuery] string url)
        {
            return Ok();
        }
    }
}