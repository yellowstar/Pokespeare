using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using Pokespeare.Service.Interfaces;
using Pokespeare.Models;
using Pokespeare.Controllers;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pokespeare.Tests
{
	public class ControllerTests
	{
		private Mock<IPokespeareWorker> _mockPokespeareWorker;
		private ILoggerFactory _mockLoggerFactory;

		[SetUp]
		public void Setup()
		{
			_mockPokespeareWorker = new Mock<IPokespeareWorker>(MockBehavior.Strict);
			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
		}

		[Test]
		public async Task GivenOKTranslationResult_WhenTranslated_ThenControllerShouldReturnOKResultWithContent()
		{
			// Arrange
			string pokemonName = "Test Pokemon";
			Translation translation = Translation.Shakespeare; 
			TranslatedPokemon translatedPokemon = new TranslatedPokemon();

			_mockPokespeareWorker.Setup(pw => pw.TranslatePokemon(It.IsAny<string>(), It.IsAny<Models.Translation>())).ReturnsAsync(new ServiceResult<TranslatedPokemon>(Result.OK, string.Empty, translatedPokemon));
			var controller = new PokemonController(_mockLoggerFactory, _mockPokespeareWorker.Object);

			// Act
			var result = await controller.Index(pokemonName, translation.ToString());

			// Assert
			var value = result.Should().BeOkObjectResult().ValueAs<TranslatedPokemon>();
			value.Should().Be(translatedPokemon);
		}

		[Test]
		public async Task GivenNotFoundTranslationResult_WhenTranslated_ThenControllerShouldReturnNotFound()
		{
			// Arrange
			string pokemonName = "Test Pokemon";
			Translation translation = Translation.Shakespeare;

			_mockPokespeareWorker.Setup(pw => pw.TranslatePokemon(It.IsAny<string>(), It.IsAny<Models.Translation>())).ReturnsAsync(new ServiceResult<TranslatedPokemon>(Result.NotFound, $"{pokemonName} not found", null));
			var controller = new PokemonController(_mockLoggerFactory, _mockPokespeareWorker.Object);

			// Act
			var result = await controller.Index(pokemonName, translation.ToString());

			// Assert
			var value = result.Should().BeNotFoundResult();
		}

		[Test]
		public async Task GivenNoTranslationResult_WhenTranslated_ThenControllerShouldReturnNoContent()
		{
			// Arrange
			string pokemonName = "Test Pokemon";
			Translation translation = Translation.Shakespeare;

			_mockPokespeareWorker.Setup(pw => pw.TranslatePokemon(It.IsAny<string>(), It.IsAny<Models.Translation>())).ReturnsAsync(new ServiceResult<TranslatedPokemon>(Result.NoTranslation, $"No translation for {translation}", null));
			var controller = new PokemonController(_mockLoggerFactory, _mockPokespeareWorker.Object);

			// Act
			var result = await controller.Index(pokemonName, translation.ToString());

			// Assert
			var value = result.Should().BeNoContentResult();
		}

		[Test]
		public async Task GivenErrorResult_WhenTranslated_ThenControllerShouldReturnBadRequest()
		{
			// Arrange
			string pokemonName = "Test Pokemon";
			Translation translation = Translation.Shakespeare;

			_mockPokespeareWorker.Setup(pw => pw.TranslatePokemon(It.IsAny<string>(), It.IsAny<Models.Translation>())).ReturnsAsync(new ServiceResult<TranslatedPokemon>(Result.Error, $"No translation for {translation}", null));
			var controller = new PokemonController(_mockLoggerFactory, _mockPokespeareWorker.Object);

			// Act
			var result = await controller.Index(pokemonName, translation.ToString());

			// Assert
			var value = result.Should().BeBadRequestResult();
		}


		[Test]
		public async Task GivenInvalidTranslation_WhenTranslated_ThenControllerShouldReturnNoContent()
		{
			// Arrange
			string pokemonName = "Test Pokemon";
			string translation = "Invalid";

			_mockPokespeareWorker.Setup(pw => pw.TranslatePokemon(It.IsAny<string>(), It.IsAny<Models.Translation>())).ReturnsAsync(new ServiceResult<TranslatedPokemon>(Result.NoTranslation, $"No translation for {translation}", null));
			var controller = new PokemonController(_mockLoggerFactory, _mockPokespeareWorker.Object);

			// Act
			var result = await controller.Index(pokemonName, translation);

			// Assert
			var value = result.Should().BeNoContentResult();
		}
	}
}
