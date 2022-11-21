using PokemonApi.Common.Models;
using PokemonApi.Repository.Interfaces;
using PokemonApi.Services.Interfaces;
using System.Text.Json;

namespace PokemonApi.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IEnumerable<ITranslationRespository> _translationrepositories;

        public TranslationService(IEnumerable<ITranslationRespository> translationrepositories)
        {
            _translationrepositories = translationrepositories;
        }

        public async Task<string?> GetTranslation(string description, string? habitat, bool? isLegendary)
        {
            ITranslationRespository? translationRepository;
            if (string.Equals(habitat, "cave", StringComparison.OrdinalIgnoreCase) || (isLegendary.HasValue && isLegendary.Value == true))
            {
                translationRepository = _translationrepositories?.SingleOrDefault(x => x.TranslationType == Repository.Enums.TranslationType.Yoda);
                if (translationRepository == null)
                {
                    throw new Exception("Yoda translator has not been implemented.");
                }
            }
            else
            {
                translationRepository = _translationrepositories?.SingleOrDefault(x => x.TranslationType == Repository.Enums.TranslationType.Shakespeare);
                if (translationRepository == null)
                {
                    throw new Exception("Shakespeare translator has not been implemented.");
                }
            }

            var httpResponseMessage = await translationRepository.GetTranslation(description);
           
            var translationResult = await DeserializeTranslationAsync(httpResponseMessage);

            if (translationResult?.Success?.Total != 1)
            {
                return description;
            }

            return translationResult?.Contents?.Translated;
        }

        private static async Task<TranslationResult?> DeserializeTranslationAsync(HttpResponseMessage httpResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<TranslationResult>(await httpResponseMessage.Content.ReadAsStringAsync(), options);
        }

    }
}
