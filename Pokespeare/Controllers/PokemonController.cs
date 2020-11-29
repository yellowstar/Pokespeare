using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Service.Interfaces;
using System;
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
		public async Task<IActionResult> Index([FromRoute(Name = "pokemonName")] string pokemonName, [FromRoute(Name = "translation")] string translation)
		{
			_logger.LogDebug($"Pokemon translation invoked with Pokemon name: {pokemonName}");

			Translation t;

			if (string.IsNullOrWhiteSpace(translation))
			{
				// If no specific 'language' is selected default to Shakespeare
				t = Translation.Shakespeare;
			}
			else
			{
				if (!Enum.TryParse<Translation>(translation, true, out t))
				{
					return NoContent();
				}
			}

			var result = await _pokespeareWorker.TranslatePokemon(pokemonName, t);

			if (result == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}

			_logger.LogDebug($"{result.Result}: {result.Message}");

			switch (result.Result)
			{
				case Result.OK:
					{
						return Ok(result.Content);
					}
				case Result.NotFound:
					{
						return NotFound();
					}
				case Result.NoTranslation:
					{
						return NoContent();
					}
				case Result.Error:
					{
						return BadRequest();
					}
				default:
					{
						return StatusCode(StatusCodes.Status500InternalServerError);
					}
			}

		}
	}
}
