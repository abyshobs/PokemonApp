using PokemonApi.Services.Interfaces;
using Shouldly;

namespace PokemonApi.IntegrationTest
{
    public class WhenGettingFunTranslation : IClassFixture<HostBuilderFixture<Program>>
    {
        private readonly HostBuilderFixture<Program> _fixture;
        private readonly ITranslationService _translationService;

        public WhenGettingFunTranslation(HostBuilderFixture<Program> fixture)
        {
            _fixture = fixture;
            _translationService = _fixture.GetService<ITranslationService>();
        }

        [Theory]
        [InlineData("cave", false)]
        [InlineData("cave", true)]
        [InlineData("rare", false)]
        public async Task AndPokemonMeetsYodaTranslationCriteria_ThenYodaTranslationShouldBeReturned(string habitat, bool isLegendary)
        {
            var testDescription = "Master Obiwan has lost a planet.";
            var translatedText = await _translationService.GetTranslation(testDescription, habitat, isLegendary);
            translatedText?.ShouldNotBeNull();
            translatedText?.ToLower().ShouldBe("lost a planet,  master obiwan has.");
        }

        [Theory]
        [InlineData("rare", false)]
        public async Task AndPokemonMeetsShakespeareTranslationCriteria_ThenShakespeareTranslationShouldBeReturned(string habitat, bool isLegendary)
        {
            var testDescription = "Master Obiwan has lost a planet.";
            var translatedText = await _translationService.GetTranslation(testDescription, habitat, isLegendary);
            translatedText?.ShouldNotBeNull();
            translatedText?.ToLower().ShouldBe("master obiwan hath did lose a planet.");
        }

        [Fact]
        public async Task AndPokemonMeetsNoCriteria_ThenStandardTranslationShouldBeReturned()
        {
            var testDescription = "Master Obiwan has lost a planet.";
            var translatedText = await _translationService.GetTranslation(testDescription, null, null);
            translatedText?.ShouldNotBeNull();
            translatedText?.ToLower().ShouldBe("master obiwan has lost a planet.");
        }
    }
}