using PokemonApi.Common.Models;

namespace PokemonApi.Services
{
    public interface IPokemonService
    {
        Task<Pokemon?> GetPokemonDetailsAsync(string name);
        Task<PokemonSpecies?> GetPokemonSpeciesAsync(Uri? speciesUri);
    }
}