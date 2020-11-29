using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokeApiNet.Interfaces;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Providers.Pokemon;
using System.Collections.Generic;
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
			_mockTranslationApi.Setup(t => t.Translate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ApiTranslationResponse());
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
	}
}