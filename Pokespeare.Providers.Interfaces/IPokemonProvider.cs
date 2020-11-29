using PokeApiNet;
using Pokespeare.Models;
using System.Threading.Tasks;

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
		/// <param name="pokemonName">The name of the Pokemon for which the details are to be retrieved, wrapped in a ServiceResult to indicate if the request was successful or not.</param>
		/// <returns></returns>
		Task<ServiceResult<PokeApiNet.Pokemon>> GetPokemon(string pokemonName);

		/// <summary>
		/// Retrieves details of the specified pokemon species
		/// </summary>
		/// <param name="pokemonSpecies">Name and Url of the requested species</param>
		/// <returns>The requested Pokemon species, wrapped in a ServiceResult to indicate if the request was successful or not.</returns>
		Task<ServiceResult<PokeApiNet.PokemonSpecies>> GetPokemonSpecies(NamedApiResource<PokemonSpecies> pokemonSpecies);
	}
}