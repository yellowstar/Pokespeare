using NUnit.Framework;
using Pokespeare.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Pokespeare.Tests
{
	public class PokespeareWorkerTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test, TestCaseSource(typeof(PokespeareWorkerTestCaseFactory), "ValidPokespeareWorkerCases")]
		public async Task<string> GivenValidData_WhenRequested_ThenWorkerReturnsTranslatedItem(IPokespeareWorker pokespeareWorker, string pokemonName, Models.Translation requestedTranslation)
		{
			// Act
			var translatedPokemon = await pokespeareWorker.TranslatePokemon(pokemonName, requestedTranslation);

			// Assert
			return translatedPokemon.Content.Description.FirstOrDefault();
		}
	}
}