using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Service.Interfaces;
using System;
using System.Collections.Generic;
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
			throw new NotImplementedException();
		}
	}
}
