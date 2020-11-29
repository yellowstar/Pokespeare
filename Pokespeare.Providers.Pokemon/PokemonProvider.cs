using Microsoft.Extensions.Logging;
using PokeApiNet.Interfaces;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PokeApiNet;

namespace Pokespeare.Providers.Pokemon
{
	public class PokemonProvider : IPokemonProvider
	{
		private readonly ILoggerFactory _loggerFactory;
		private readonly ILogger<PokemonProvider> _logger;
		private readonly IPokeApiClient _pokeApiClient;

		public PokemonProvider(ILoggerFactory loggerFactory, IPokeApiClient pokeApiClient)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<PokemonProvider>();
			_pokeApiClient = pokeApiClient;
		}

		public async Task<ServiceResult<PokeApiNet.Pokemon>> GetPokemon(string pokemonName)
		{
			_logger.LogDebug($"Fetching details for {pokemonName} Pokemon");
			ServiceResult<PokeApiNet.Pokemon> pokemonResult = null;

			try
			{
				var pokemon = await _pokeApiClient.GetResourceAsync<PokeApiNet.Pokemon>(pokemonName);

				pokemonResult = new ServiceResult<PokeApiNet.Pokemon>(Result.OK, string.Empty, pokemon);
			}
			catch (Exception e)
			{
				_logger.LogError($"Error retrieving {pokemonName}: {e.Message} @ {e.StackTrace}");
				pokemonResult = new ServiceResult<PokeApiNet.Pokemon>(Result.Error, e.Message, null);
			}

			return pokemonResult;
		}

		public async Task<ServiceResult<PokemonSpecies>> GetPokemonSpecies(NamedApiResource<PokemonSpecies> pokemonSpecies)
		{
			_logger.LogDebug($"Fetching species details for {pokemonSpecies.Name}");
			ServiceResult<PokemonSpecies> speciesResult = null;

			try
			{
				var species = await _pokeApiClient.GetResourceAsync(pokemonSpecies);

				speciesResult = new ServiceResult<PokemonSpecies>(Result.OK, string.Empty, species);
			}
			catch (Exception e)
			{
				_logger.LogError($"Error retrieving {pokemonSpecies.Name}: {e.Message} @ {e.StackTrace}");
				speciesResult = new ServiceResult<PokemonSpecies>(Result.Error, e.Message, null);
			}

			return speciesResult;
		}
	}
}
