using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokespeare.Service
{
	public class PokespeareWorker : IPokespeareWorker
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly IPokemonProvider _pokemonProvider;
		private readonly ILogger<PokespeareWorker> _logger;
		private readonly IEnumerable<ITranslationProvider> _translationProviders;

		public PokespeareWorker(ILoggerFactory loggerFactory, IPokemonProvider pokemonProvider, IEnumerable<ITranslationProvider> translationProviders)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<PokespeareWorker>();
			_pokemonProvider = pokemonProvider;
			_translationProviders = translationProviders;
		}

		public async Task<ServiceResult<TranslatedPokemon>> TranslatePokemon(string pokemonName, Translation translation)
		{
			ServiceResult<TranslatedPokemon> result = null;

			ITranslationProvider _translationProvider = _translationProviders.FirstOrDefault(t => t.SupportsTranslation(translation));

			// Check it the translationProvider is available
			if (_translationProvider == null)
			{
				result = new ServiceResult<TranslatedPokemon>(Result.NoTranslation, $"Could not find a translator for {translation}", null);
				return result;
			}

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

			// Only take the first item because of the rate limiting on the public translation API
			var enAbilityFlavorText = speciesResult.Content.FlavorTextEntries.Where(f => string.Equals(f.Language.Name, "en", StringComparison.InvariantCultureIgnoreCase)).Take(1);

			var translationResult = await _translationProvider.TranslatedPokemon(enAbilityFlavorText.Select(e => new TranslationRequest(e.Version.Name, e.FlavorText)));

			result = new ServiceResult<TranslatedPokemon>(Result.OK, string.Empty, new TranslatedPokemon(pokemonName, translationResult.Select(t => t.TranslatedText).ToList()));

			return result;
		}
	}
}