using Microsoft.Extensions.Logging;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokespeare.Providers.Translation
{
	public class VulcanTranslator : TranslationBase, ITranslationProvider
	{
		public VulcanTranslator(ILoggerFactory loggerFactory, ITranslationApi translationApi) : base(translationApi, loggerFactory, Models.Translation.Vulcan, "vulcan")
		{
		}

		public new bool SupportsTranslation(Models.Translation translation)
		{
			return base.SupportsTranslation(translation);
		}

		public async Task<IEnumerable<TranslationResponse>> TranslatedPokemon(IEnumerable<TranslationRequest> translationRequests)
		{
			List<TranslationResponse> translationResponses = new List<TranslationResponse>();

			foreach (var translationRequest in translationRequests)
			{
				var translationResponse = await TranslateText(translationRequest.RequestText);
				if (!string.IsNullOrWhiteSpace(translationResponse))
				{
					translationResponses.Add(new TranslationResponse(translationRequest.Property, translationResponse));
				}
			}

			return translationResponses;
		}
	}
}