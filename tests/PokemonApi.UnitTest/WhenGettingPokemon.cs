using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PokemonApi.Common.Models;
using PokemonApi.Models;
using PokemonApi.Services.Interfaces;
using PokemonApp.Controllers;
using Shouldly;

namespace PokemonApi.UnitTest
{
    public class WhenGettingPokemon
    {
        private readonly PokemonController _sut;
        private readonly Mock<IPokemonService> _mockPokemonService;

        public WhenGettingPokemon()
        {
            _mockPokemonService = new Mock<IPokemonService>();
            _sut = new PokemonController(_mockPokemonService.Object, null, NullLogger<PokemonController>.Instance);
        }

        [Fact]
        public async Task ThenNewLineCharactersShouldBeRemovedFromPokemonDescription()
        {
            // Arrange           
            var language = new Language { Name = "en" };
            var englishDescription = new FlavorText
            {
                Language = language,
                Flavor_Text = "Hello\nI am a\fstandard pokemon."
            };

            var testPokemonName = "testName";
            var testPokemonSpecies = new PokemonSpecies { Flavor_Text_Entries = new List<FlavorText> { englishDescription } };
            _mockPokemonService.Setup(x => x.GetPokemonDetailsAsync(testPokemonName)).ReturnsAsync(new Pokemon { Id = 1, Species = It.IsAny<PokemonSpeciesLink>() });
            _mockPokemonService.Setup(x => x.GetPokemonSpeciesAsync(It.IsAny<Uri>())).ReturnsAsync(testPokemonSpecies);

            // Act
            var result = await _sut.Get(testPokemonName);
            var okResult = result as OkObjectResult;

            // Assert
            okResult?.StatusCode.ShouldBe(200);
            okResult?.Value.ShouldNotBeNull();
            var pokemonModel = okResult.Value as PokemonModel;
            pokemonModel.ShouldNotBeNull();
            pokemonModel.Description.ShouldBe("Hello I am a standard pokemon.");
        }

        [Fact]
        public async Task NonExistentPokemonShouldReturnNotFound()
        {
            // Arrange           
            var language = new Language { Name = "en" };
            var englishDescription = new FlavorText
            {
                Language = language,
                Flavor_Text = "Hello\nI am a\fstandard pokemon."
            };

            var testPokemonName = "testName";
            var testPokemonSpecies = new PokemonSpecies { Flavor_Text_Entries = new List<FlavorText> { englishDescription } };
            _mockPokemonService.Setup(x => x.GetPokemonDetailsAsync("nonExistentName")).ReturnsAsync(new Pokemon { Id = 1, Species = It.IsAny<PokemonSpeciesLink>() });
            _mockPokemonService.Setup(x => x.GetPokemonSpeciesAsync(It.IsAny<Uri>())).ReturnsAsync(testPokemonSpecies);

            // Act
            var result = await _sut.Get(testPokemonName);
            var okResult = result as OkObjectResult;

            // Assert
            okResult?.StatusCode.ShouldBe(404);
        }

        [Fact]
        public async Task NonExistentPokemonSpeciesShouldReturnNotFound()
        {
            // Arrange                      
            var testPokemonName = "testName";
            _mockPokemonService.Setup(x => x.GetPokemonDetailsAsync(testPokemonName)).ReturnsAsync(new Pokemon { Id = 1, Species = It.IsAny<PokemonSpeciesLink>() });
            _mockPokemonService.Setup(x => x.GetPokemonSpeciesAsync(It.IsAny<Uri>())).ReturnsAsync(It.IsAny<PokemonSpecies>());

            // Act
            var result = await _sut.Get(testPokemonName);
            var okResult = result as OkObjectResult;

            // Assert
            okResult?.StatusCode.ShouldBe(404);
        }
    }
}