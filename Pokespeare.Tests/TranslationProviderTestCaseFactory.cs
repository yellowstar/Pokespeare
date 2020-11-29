using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Providers.Translation;
using System.Collections;

namespace Pokespeare.Tests
{
	public class TranslationProviderTestCaseFactory
	{
		private static ILoggerFactory _mockLoggerFactory;
		private static Mock<ITranslationApi> _mockTranslationApi;

		public static void SetInjectedProperties()
		{
			_mockTranslationApi = new Mock<ITranslationApi>(MockBehavior.Strict);
			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
			_mockTranslationApi.Setup(t => t.Translate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ApiTranslationResponse());
		}

		public static IEnumerable ValidTranslationSupportCases
		{
			get
			{
				SetInjectedProperties();
				yield return new TestCaseData(new ShakespeareTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Shakespeare).Returns(true);
				yield return new TestCaseData(new YodaTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Yoda).Returns(true);
				yield return new TestCaseData(new VulcanTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Vulcan).Returns(true);
			}
		}

		public static IEnumerable InValidTranslationSupportCases
		{
			get
			{
				SetInjectedProperties();
				yield return new TestCaseData(new ShakespeareTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Yoda).Returns(false);
				yield return new TestCaseData(new YodaTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Vulcan).Returns(false);
				yield return new TestCaseData(new VulcanTranslator(_mockLoggerFactory, _mockTranslationApi.Object), Translation.Shakespeare).Returns(false);
			}
		}
	}
}