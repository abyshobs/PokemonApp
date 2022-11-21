using PokemonApi.Repository.Interfaces;

namespace PokemonApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PokemonRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> GetPokemonDetailsByNameAsync(string name)
        {
            var httpClient = _httpClientFactory.CreateClient("PokeApi");
            return await httpClient.GetAsync(httpClient.BaseAddress + $"pokemon/{name}/");
        }

        public async Task<HttpResponseMessage> GetPokemonSpeciesAsync(Uri? speciesUri)
        {
            var httpClient = _httpClientFactory.CreateClient("PokeApi");
            return await httpClient.GetAsync(speciesUri);
        }
    }
}