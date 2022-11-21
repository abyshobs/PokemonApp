using PokemonApi.Repository.Interfaces;
using PokemonApi.Services.Interfaces;

namespace PokemonApi.IntegrationTest
{
    public class WhenGettingFunTranslation : IClassFixture<HostBuilderFixture<Program>>
    {
        private readonly HostBuilderFixture<Program> _fixture;

        public WhenGettingFunTranslation(HostBuilderFixture<Program> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Test1()
        {
            var translatorService = _fixture.GetService<ITranslationService>();
            var translatedText = await translatorService.GetTranslation("Master Obiwan has lost a planet.", "cave", true);
            translatedText?.ShouldNotBeNull();
        }
    }
}