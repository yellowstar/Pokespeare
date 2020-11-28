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
using PokeApiNet.Interfaces;

namespace Pokespeare.Tests
{
	public class PokemonProviderTests
	{
		private Mock<IPokeApiClient> _mockPokeApiClient;
		private ILoggerFactory _mockLoggerFactory;

		[SetUp]
		public void Setup()
		{
			_mockPokeApiClient = new Mock<IPokeApiClient>(MockBehavior.Strict);
			_mockLoggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
		}

		[Test]
		public async Task GivenOKTranslationResult_WhenTranslated_ThenControllerShouldReturnOKResultWithContent()
		{ }
	}
}
