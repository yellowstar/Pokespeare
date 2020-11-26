using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pokespeare.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly ILogger<PokemonController> _logger;

		public PokemonController(ILogger<PokemonController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{pokemonName}/{translation?}")]
		public IActionResult Index([FromRoute(Name = "pokemonName")] string PokemonName, [FromRoute(Name = "translation")] string Translation)
		{
			return Ok();
		}
	}
}
