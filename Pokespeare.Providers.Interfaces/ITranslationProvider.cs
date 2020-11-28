using Pokespeare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokespeare.Providers.Interfaces
{
	/// <summary>
	/// Interface describing the different 'language' translation classes so the required one can be dynamically selected
	/// </summary>
	public interface ITranslationProvider
	{
		/// <summary>
		/// Flag to indicate if a specific provider supports the requested language
		/// </summary>
		/// <param name="translation">A Translation enum indicating the required language</param>
		/// <returns>bool to indicate if the required language is supported</returns>
		bool SupportsLanguage(Translation translation);

		/// <summary>
		/// Translates the given items into the required language that is handled by the specific translation provider
		/// </summary>
		/// <param name="translationRequests">A list of text items to be translated</param>
		/// <returns>An list of translated text items</returns>
		IEnumerable<TranslationResponse> TranslatedPokemon(IEnumerable<TranslationRequest> translationRequests);
	}
}
