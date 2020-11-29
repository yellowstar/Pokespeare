using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokeApiNet.Interfaces;
using Pokespeare.Models;
using Pokespeare.Providers.Pokemon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokespeare.Tests
{
	public class PokemonProviderTests
	{
		private Mock<IPokeApiClient> _mockPokeApiClient;
		private ILoggerFactory _mockLoggerFactory;

		[SetUp]
		public void Setup()
		{
			_mockPokeApiClient = new Mock<IPokeApiClient>(MockBehavior.Loose);  // Ideally this would be strict, but for the sake of brevity setting to Loose in this instance
			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
		}

		[Test]
		public async Task GivenOKResult_WhenResourceRequested_ThenProviderShouldReturnOKResultWithPokemonContent()
		{
			//Arrange
			string pokemonName = "testemon";
			PokeApiNet.Pokemon testPokemon = new PokeApiNet.Pokemon()
			{
				Id = 1,
				Name = pokemonName,
				Height = 123,
				IsDefault = true
			};

			_mockPokeApiClient.Setup(p => p.GetResourceAsync<PokeApiNet.Pokemon>(It.IsAny<string>())).ReturnsAsync(testPokemon);
			var pokemonProvider = new PokemonProvider(_mockLoggerFactory, _mockPokeApiClient.Object);

			// Act
			var pokemonResult = await pokemonProvider.GetPokemon(pokemonName);

			// Assert
			pokemonResult.Should().BeOfType(typeof(ServiceResult<PokeApiNet.Pokemon>));
			pokemonResult.Content.Should().NotBeNull();
			pokemonResult.Content.Name.Should().Be(pokemonName);
		}

		[Test]
		public async Task GivenOKResult_WhenResourceRequested_ThenProviderShouldReturnOKResultWithSpeciesContent()
		{
			// Arrange
			var speciesName = "Species 2";
			PokeApiNet.PokemonSpecies testPokemonSpecies = new PokeApiNet.PokemonSpecies()
			{
				Id = 2,
				Name = speciesName,
				Order = 1,
				FlavorTextEntries = new List<PokeApiNet.PokemonSpeciesFlavorTexts>()
				{
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = "Flavor text 1",
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "en",
							Url = "url"
						}
					},
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = "Flavor text 2",
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "en",
							Url = "url"
						}
					},
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = "Flavor text 3",
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "fr",
							Url = "url"
						}
					}
				}
			};

			var speciesRequest = new PokeApiNet.NamedApiResource<PokeApiNet.PokemonSpecies>() { Name = "test species", Url = "test url" };

			_mockPokeApiClient.Setup(p => p.GetResourceAsync(It.IsAny<PokeApiNet.UrlNavigation<PokeApiNet.PokemonSpecies>>())).ReturnsAsync(testPokemonSpecies);
			var pokemonProvider = new PokemonProvider(_mockLoggerFactory, _mockPokeApiClient.Object);

			// Act
			var speciesResult = await pokemonProvider.GetPokemonSpecies(speciesRequest);

			// Assert
			speciesResult.Should().BeOfType(typeof(ServiceResult<PokeApiNet.PokemonSpecies>));
			speciesResult.Content.Should().NotBeNull();
			speciesResult.Content.Name.Should().Be(speciesName);
			speciesResult.Content.FlavorTextEntries.Count.Should().Be(3);
		}
	}
}