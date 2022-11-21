namespace PokemonApi.Repository.Interfaces
{
    public interface IPokemonRepository
    {
        Task<HttpResponseMessage> GetPokemonDetailsByNameAsync(string name);
        Task<HttpResponseMessage> GetPokemonSpeciesAsync(Uri? speciesUri);
    }
}
