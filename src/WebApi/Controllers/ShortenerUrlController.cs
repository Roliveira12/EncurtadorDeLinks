using Application.UseCases;
using Application.UseCases.Boundaries.ShortenerUrl;
using Application.UseCases.CreateShortenerUrl;
using Application.UseCases.GetShortenerUrlUseCase;
using Application.UseCases.GetShortenerUrlUseCase.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortenerUrlController : ControllerBase
    {
        [HttpGet("topurls")]
        [ProducesResponseType(typeof(IEnumerable<ShortenerUrlOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetTopItems([FromServices] IGetShortenerUrlUseCase useCase, [FromQuery] int top = 5, [FromQuery] OrderByType orderBy = OrderByType.Descending, CancellationToken cancellationToken = default)
        {
            var result = await useCase.GetTopItemsAsync(top, orderBy, cancellationToken);

            if (!result.Any())
            {
                return new NoContentResult();
            }

            return new OkObjectResult(new OkObjectResult(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ShortenerUrlOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromServices] IGetShortenerUrlUseCase useCase, string urlId, CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(urlId, cancellationToken);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new RedirectResult(result.Url);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShortenerUrlOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortUrlAsync([FromServices] ICreateShortenerUrlUseCase useCase, [FromBody] string url, CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(url, cancellationToken);

            return Created("", result);
        }
    }
}