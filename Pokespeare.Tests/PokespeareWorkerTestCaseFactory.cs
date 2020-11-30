using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokespeare.Models;
using Pokespeare.Providers.Interfaces;
using Pokespeare.Service;
using System.Collections;
using System.Collections.Generic;

namespace Pokespeare.Tests
{
	public class PokespeareWorkerTestCaseFactory
	{
		private static ILoggerFactory _mockLoggerFactory;
		private static Mock<IPokemonProvider> _mockPokemonProvider;
		private static List<ITranslationProvider> _mockTranslationProviders;
		private static string originalText = "Outside of a dog, a book is a man's best friend. Inside of a dog its too dark to read";
		private static string shakespeareTranslation = "Outside of a dog, a booketh is a sir's most wondrous cousin. Inside of a dog tis too dark to read";
		private static string yodaTranslation = "Outside of a dog, a man's best friend, a book is. Inside of a dog its too dark to read";
		private static string vulcanTranslation = "Si' wuh dog, wuh dunap nam-tor wuh sasu's buhfik t'hy'la. Svi' wuh dog its nuh' gu'gelik tor telv-tor";
		private static string propertyName = "description";
		private static string pokemonName = "testichu";
		private static string speciesName = "pokeSpecies";

		private static void SetInitialState()
		{
			_mockPokemonProvider = new Mock<IPokemonProvider>(MockBehavior.Strict);
			_mockPokemonProvider.Setup(p => p.GetPokemon(It.IsAny<string>())).ReturnsAsync(new ServiceResult<PokeApiNet.Pokemon>(Result.OK, string.Empty, new PokeApiNet.Pokemon()
			{
				Id = 1,
				Name = pokemonName,
				Height = 123,
				IsDefault = true
			}));

			_mockPokemonProvider.Setup(p => p.GetPokemonSpecies(It.IsAny<PokeApiNet.NamedApiResource<PokeApiNet.PokemonSpecies>>())).ReturnsAsync(new ServiceResult<PokeApiNet.PokemonSpecies>(Result.OK, string.Empty, new PokeApiNet.PokemonSpecies()
			{
				Id = 2,
				Name = speciesName,
				Order = 1,
				FlavorTextEntries = new List<PokeApiNet.PokemonSpeciesFlavorTexts>()
				{
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = originalText,
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "en",
							Url = "url"
						},
						Version = new PokeApiNet.NamedApiResource<PokeApiNet.Version>()
						{
							Name = "version 1",
							Url = "version url"
						}
					},
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = "Flavor text 2",
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "en",
							Url = "url"
						},
						Version = new PokeApiNet.NamedApiResource<PokeApiNet.Version>()
						{
							Name = "version 1",
							Url = "version url"
						}
					},
					new PokeApiNet.PokemonSpeciesFlavorTexts()
					{
						FlavorText = "Flavor text 3",
						Language = new PokeApiNet.NamedApiResource<PokeApiNet.Language>()
						{
							Name = "fr",
							Url = "url"
						},
						Version = new PokeApiNet.NamedApiResource<PokeApiNet.Version>()
						{
							Name = "version 1",
							Url = "version url"
						}
					}
				}
			}));

			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

			Mock<ITranslationProvider> vulcanProvider = new Mock<ITranslationProvider>(MockBehavior.Loose);
			vulcanProvider.Setup(s => s.SupportsTranslation(It.Is<Translation>(t => t == Translation.Vulcan))).Returns(true);
			vulcanProvider.Setup(s => s.TranslatedPokemon(It.IsAny<IEnumerable<TranslationRequest>>()))
				.ReturnsAsync(new List<TranslationResponse>() { new TranslationResponse(propertyName, vulcanTranslation) });

			Mock<ITranslationProvider> shakespeareProvider = new Mock<ITranslationProvider>(MockBehavior.Loose);
			shakespeareProvider.Setup(s => s.SupportsTranslation(It.Is<Translation>(t => t == Translation.Shakespeare))).Returns(true);
			shakespeareProvider.Setup(s => s.TranslatedPokemon(It.IsAny<IEnumerable<TranslationRequest>>()))
				.ReturnsAsync(new List<TranslationResponse>() { new TranslationResponse(propertyName, shakespeareTranslation) });

			Mock<ITranslationProvider> yodaProvider = new Mock<ITranslationProvider>(MockBehavior.Loose);
			yodaProvider.Setup(s => s.SupportsTranslation(It.Is<Translation>(t => t == Translation.Yoda))).Returns(true);
			yodaProvider.Setup(s => s.TranslatedPokemon(It.IsAny<IEnumerable<TranslationRequest>>()))
				.ReturnsAsync(new List<TranslationResponse>() { new TranslationResponse(propertyName, yodaTranslation) });

			_mockTranslationProviders = new List<ITranslationProvider>();
			_mockTranslationProviders.Add(shakespeareProvider.Object);
			_mockTranslationProviders.Add(vulcanProvider.Object);
			_mockTranslationProviders.Add(yodaProvider.Object);
		}

		public static IEnumerable ValidPokespeareWorkerCases
		{
			get
			{
				SetInitialState();
				yield return new TestCaseData(new PokespeareWorker(_mockLoggerFactory, _mockPokemonProvider.Object, _mockTranslationProviders), pokemonName, Translation.Shakespeare).Returns(shakespeareTranslation);
				yield return new TestCaseData(new PokespeareWorker(_mockLoggerFactory, _mockPokemonProvider.Object, _mockTranslationProviders), pokemonName, Translation.Yoda).Returns(yodaTranslation);
				yield return new TestCaseData(new PokespeareWorker(_mockLoggerFactory, _mockPokemonProvider.Object, _mockTranslationProviders), pokemonName, Translation.Vulcan).Returns(vulcanTranslation);
			}
		}
	}
}