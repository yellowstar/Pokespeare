using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokespeare.Service
{
	public class PokespeareWorker : IPokespeareWorker
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly IPokemonProvider _pokemonProvider;
		private readonly ILogger<PokespeareWorker> _logger;
		public PokespeareWorker(ILoggerFactory loggerFactory, IPokemonProvider pokemonProvider)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<PokespeareWorker>();
			_pokemonProvider = pokemonProvider;
		}

		public async Task<ServiceResult<TranslatedPokemon>> TranslatePokemon(string pokemonName, Translation translation)
		{
			ServiceResult<TranslatedPokemon> result = null;

			var pokemonResult = await _pokemonProvider.GetPokemon(pokemonName);

			if (pokemonResult == null || pokemonResult.Result != Result.OK)
			{
				return new ServiceResult<TranslatedPokemon>(
					pokemonResult != null ? pokemonResult.Result : Result.Error,
					pokemonResult != null ? pokemonResult.Message : "Error retrieving Pokemon", 
					null
				);
			}

			var speciesResult = await _pokemonProvider.GetPokemonSpecies(pokemonResult.Content.Species);
			if (speciesResult == null || speciesResult.Result != Result.OK)
			{
				return new ServiceResult<TranslatedPokemon>(
					speciesResult != null ? speciesResult.Result : Result.Error,
					speciesResult != null ? speciesResult.Message : "Error retrieving Pokemon Species",
					null
				);
			}

			var enAbilityFlavorText = speciesResult.Content.FlavorTextEntries.Where(f => string.Equals(f.Language.Name, "en", StringComparison.InvariantCultureIgnoreCase));

			// Now translate
			result = new ServiceResult<TranslatedPokemon>(Result.OK, string.Empty, new TranslatedPokemon() { });

			return result;
		}
	}
}
