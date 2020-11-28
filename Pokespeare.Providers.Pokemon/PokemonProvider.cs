using Microsoft.Extensions.Logging;
using PokeApiNet.Interfaces;
using Pokespeare.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

		public Task<PokeApiNet.Pokemon> GetPokemon(string pokemonName)
		{
			throw new NotImplementedException();
		}
	}
}
