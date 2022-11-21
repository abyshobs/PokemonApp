using PokemonApi.Common.Models;
using PokemonApi.Repository.Interfaces;
using PokemonApi.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace PokemonApi.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task<Pokemon?> GetPokemonDetailsAsync(string name)
        {
            var httpResponseMessage = await _pokemonRepository.GetPokemonDetailsByNameAsync(name);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await DeserializeAsync<Pokemon>(httpResponseMessage);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception(await httpResponseMessage.Content.ReadAsStringAsync());
            }
        }

        public async Task<PokemonSpecies?> GetPokemonSpeciesAsync(Uri? speciesUri)
        {
            var httpResponseMessage = await _pokemonRepository.GetPokemonSpeciesAsync(speciesUri);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await DeserializeAsync<PokemonSpecies>(httpResponseMessage);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception(await httpResponseMessage.Content.ReadAsStringAsync());
            }
        }

        private static async Task<T?> DeserializeAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<T>(contentStream, options);
        }

    }
}
