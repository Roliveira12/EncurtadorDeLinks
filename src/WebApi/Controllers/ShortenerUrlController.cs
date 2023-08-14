using Application.UseCases.Boundaries.ShortenerUrl;
using Application.UseCases.CreateShortenerUrl;
using Application.UseCases.GetShortenerUrlUseCase;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Boundaries;

namespace WebApi.Controllers
{
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

            return new OkObjectResult(result);
        }

        [HttpGet("{urlId}")]
        [ProducesResponseType(typeof(ShortenerUrlOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RedirectAsync([FromServices] IGetShortenerUrlUseCase useCase, [FromRoute] string urlId, [FromQuery] bool redirect, CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(urlId, cancellationToken);

            if (result == null)
            {
                return new NotFoundResult();
            }

            if (redirect)
            {
                return new RedirectResult(result.Url);
            }

            return new OkObjectResult(result);
        }

        [HttpPost("shortener")]
        [ProducesResponseType(typeof(ShortenerUrlOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortUrlAsync([FromServices] ICreateShortenerUrlUseCase useCase, [FromBody] ShortenerUrlRequest request, CancellationToken cancellationToken)
        {
            var result = await useCase.ExecuteAsync(request.Url, cancellationToken);

            return Created("", result);
        }
    }
}