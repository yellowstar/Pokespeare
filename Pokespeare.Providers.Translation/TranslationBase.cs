using Microsoft.Extensions.Logging;
using Pokespeare.Providers.Interfaces;
using Refit;
using System.Threading.Tasks;

namespace Pokespeare.Providers.Translation
{
	public class TranslationBase
	{
		protected readonly Models.Translation _supportedTranslation;
		protected readonly string _translationUrl;
		protected readonly ILoggerFactory _loggerFactory;
		protected readonly ILogger<ITranslationProvider> _logger;
		private readonly ITranslationApi _translationApi;

		protected TranslationBase(ITranslationApi translationApi, ILoggerFactory loggerFactory, Models.Translation supportedTranslation, string translationUrl)
		{
			_loggerFactory = loggerFactory;
			_logger = _loggerFactory.CreateLogger<ITranslationProvider>();
			_supportedTranslation = supportedTranslation;
			_translationUrl = translationUrl;
			_translationApi = translationApi;
		}

		protected bool SupportsTranslation(Models.Translation requestedTranslation)
		{
			return requestedTranslation == _supportedTranslation;
		}

		protected async Task<string> TranslateText(string requestedText)
		{
			string translatedText = string.Empty;
			requestedText = requestedText.Replace("\n", " ").Replace("\f", " ");

			try
			{
				var translatedContent = await _translationApi.Translate(requestedText, _supportedTranslation.ToString().ToLower());
				if (translatedContent != null && translatedContent.success.total > 0)
				{
					translatedText = translatedContent.contents.translated;
				}
			}
			catch (ApiException apiException)
			{
				_logger.LogError($"Error translating text into {_supportedTranslation}; details: {apiException.Message} @ {apiException.StackTrace}");
			}

			return translatedText;
		}
	}
}