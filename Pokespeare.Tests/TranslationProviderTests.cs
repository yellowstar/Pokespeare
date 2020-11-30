using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Providers.Translation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokespeare.Tests
{
	public class TranslationProviderTests
	{
		private Mock<ITranslationApi> _mockTranslationApi;
		private ILoggerFactory _mockLoggerFactory;

		[SetUp]
		public void Setup()
		{
			_mockTranslationApi = new Mock<ITranslationApi>(MockBehavior.Strict);
			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
		}

		[Test, TestCaseSource(typeof(TranslationProviderTestCaseFactory), "ValidTranslationSupportCases")]
		public bool GivenCorrectTranslation_WhenChecked_TranslationProviderReturnsTrue(ITranslationProvider translationProvider, Models.Translation requestedTranslation)
		{
			// Act
			return translationProvider.SupportsTranslation(requestedTranslation);
		}

		[Test, TestCaseSource(typeof(TranslationProviderTestCaseFactory), "InValidTranslationSupportCases")]
		public bool GivenInCorrectTranslation_WhenChecked_TranslationProviderReturnsFalse(ITranslationProvider translationProvider, Models.Translation requestedTranslation)
		{
			// Act
			return translationProvider.SupportsTranslation(requestedTranslation);
		}

		[Test]
		public async Task GivenTextToTranslate_WhenTranslated_ThenTranslationProviderReturnsTranslation()
		{
			// Arrange
			string textToBeTranslated = "Space: the final frontier. These are the voyages of the starship Enterprise. Her ongoing mission: to explore strange new worlds, to seek out new life-forms and new civilizations; to boldly go where no one has gone before.";
			string vulcanTranslation = "Ret: wuh kim-shah frontier. Aifa nam-tor wuh halan t' wuh yel-hali enterprise. Ish-veh ongoing skrol: tor explore flekh uzh panu, tor seek si' uzh ha'kiv-shidau heh uzh sutenivaya; tor boldly hal-tor wilat kling has hal-tor fa'.";
			var apiTranslationResponse = new ApiTranslationResponse()
			{
				success = new Success()
				{
					total = 1
				},
				contents = new Contents()
				{
					translated = vulcanTranslation,
					text = textToBeTranslated,
					translation = "vulcan"
				}
			};
			_mockTranslationApi.Setup(t => t.Translate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(apiTranslationResponse);
			ITranslationProvider vulcanTranslationProvider = new VulcanTranslator(_mockLoggerFactory, _mockTranslationApi.Object);

			// Act
			var translationResult = await vulcanTranslationProvider.TranslatedPokemon(new List<TranslationRequest>() { new TranslationRequest("test property", textToBeTranslated) });

			// Assert
			translationResult.Should().NotBeNull();
			translationResult.Count().Should().Be(1);
			translationResult.First().TranslatedText.Should().Be(vulcanTranslation);
		}
	}
}