using Pokespeare.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokespeare.Service.Interfaces
{
	public interface IPokespeareWorker
	{
		Task<ServiceResult<TranslatedPokemon>> TranslatePokemon(string pokemonName, Translation translation);
	}
}
