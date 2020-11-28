using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PokeApiNet;

namespace Pokespeare.Providers.Interfaces
{
	/// <summary>
	/// Interface describing the details of the Pokemon provider class which retrieves Pokemon details
	/// </summary>
	public interface IPokemonProvider
	{
		/// <summary>
		/// Gets details of the specified Pokemon using the PokeApiNet package, which I have modified to include an interface to allow it to be injected
		/// </summary>
		/// <param name="pokemonName">The name of the Pokemon for which the details are to be retrieved</param>
		/// <returns></returns>
		Task<PokeApiNet.Pokemon> GetPokemon(string pokemonName);
	}
}
