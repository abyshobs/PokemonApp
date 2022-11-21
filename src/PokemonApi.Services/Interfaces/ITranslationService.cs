namespace PokemonApi.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string?> GetTranslation(string? description, string? habitat, bool? isLegendary);
    }
}
