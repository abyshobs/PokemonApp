using PokemonApi.Repository.Enums;
using PokemonApi.Repository.Interfaces;

namespace PokemonApi.Repository.Factories
{
    public class ShakespeareTranslator : ITranslationRespository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _translationUriSuffix = "shakespeare.json";

        public ShakespeareTranslator(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public TranslationType TranslationType { get; set; } = TranslationType.Shakespeare;

        public async Task<HttpResponseMessage> GetTranslation(string text)
        {
            var httpClient = _httpClientFactory.CreateClient("FunTranslationsApi");
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text),
            });

            return await httpClient.PostAsync(httpClient.BaseAddress + _translationUriSuffix, formContent);
        }
    }
}
