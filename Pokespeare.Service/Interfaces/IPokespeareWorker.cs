using Pokespeare.Models;
using System.Threading.Tasks;

namespace Pokespeare.Service.Interfaces
{
	/// <summary>
	/// The main worker class that retrieves the specific Pokemon and translates the required text
	/// </summary>
	public interface IPokespeareWorker
	{
		/// <summary>
		/// Translated the Pokemon by retrieving the relevant Pokemon via the Api and then translating the text via the translation Api
		/// </summary>
		/// <param name="pokemonName">string containing the name of the Pokemon</param>
		/// <param name="translation">Translation enum with the 'language' to which the Pokemon should be translated</param>
		/// <returns>The translated Pokemon</returns>
		Task<ServiceResult<TranslatedPokemon>> TranslatePokemon(string pokemonName, Translation translation);
	}
}