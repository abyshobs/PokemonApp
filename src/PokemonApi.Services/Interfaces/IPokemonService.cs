using PokemonApi.Common.Models;

namespace PokemonApi.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon?> GetPokemonDetailsAsync(string name);
        Task<PokemonSpecies?> GetPokemonSpeciesAsync(Uri? speciesUri);
    }
}