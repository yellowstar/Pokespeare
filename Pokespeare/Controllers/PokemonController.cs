using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Service.Interfaces;
using System.Threading.Tasks;

namespace Pokespeare.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PokemonController : ControllerBase
	{
		private readonly ILogger<PokemonController> _logger;
		private readonly ILoggerFactory _loggerFactory;
		private readonly IPokespeareWorker _pokespeareWorker;

		public PokemonController(ILoggerFactory loggerFactory, IPokespeareWorker pokespeareWorker)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<PokemonController>();
			_pokespeareWorker = pokespeareWorker;
		}

		[HttpGet("{pokemonName}/{translation?}")]
		public async Task<IActionResult> Index([FromRoute(Name = "pokemonName")] string PokemonName, [FromRoute(Name = "translation")] string Translation)
		{
			_logger.LogDebug($"Pokemon translation invoked with Pokemon name: {PokemonName}");

			var result = await _pokespeareWorker.TranslatePokemon(PokemonName, Models.Translation.Shakespeare);

			// Check the result type

			return Ok(result.Content);
		}
	}
}
