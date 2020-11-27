using Pokespeare.Models;
using Pokespeare.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokespeare.Service
{
	public class PokespeareWorker : IPokespeareWorker
	{
		public async Task<ServiceResult<TranslatedPokemon>> TranslatePokemon(string pokemonName, Translation translation)
		{
			throw new NotImplementedException();
		}
	}
}
