using Pokespeare.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace Pokespeare.Providers.Interfaces
{
	public interface ITranslationApi
	{
		[Get("/{language}.json")]
		[QueryUriFormat(UriFormat.UriEscaped)]
		Task<ApiTranslationResponse> Translate([Query] string text, string language);
	}
}