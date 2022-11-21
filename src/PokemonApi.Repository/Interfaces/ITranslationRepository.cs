using PokemonApi.Repository.Enums;

namespace PokemonApi.Repository.Interfaces
{
    public interface ITranslationRespository
    {
        public TranslationType TranslationType { get; set; }
        Task<HttpResponseMessage> GetTranslation(string text, CancellationToken cancellationToken);
    }
}
