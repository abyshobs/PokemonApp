using PokemonApi.Repository.Enums;
using PokemonApi.Repository.Interfaces;

namespace PokemonApi.Repository.Factories
{
    public class YodaTranslator : ITranslationRespository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _translationUriSuffix = "yoda.json";

        public YodaTranslator(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public TranslationType TranslationType { get; set; } = TranslationType.Yoda;

        public async Task<HttpResponseMessage> GetTranslation(string text, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("FunTranslationsApi");
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("text", text),
            });

            return await httpClient.PostAsync(httpClient.BaseAddress + _translationUriSuffix, formContent, cancellationToken);
        }
    }
}
