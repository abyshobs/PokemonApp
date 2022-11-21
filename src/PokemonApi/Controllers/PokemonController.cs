using Microsoft.AspNetCore.Mvc;
using PokemonApi.Common.ExtensionMethods;
using PokemonApi.Common.Models;
using PokemonApi.Models;
using PokemonApi.Services;

namespace PokedexApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonService pokemonService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [Route("{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(PokemonModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PokemonModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var pokemonSpecies = await GetPokemon(name);
                if (pokemonSpecies == null)
                {
                    return NotFound();
                }

                var pokemonModel = GetPokemonModel(name, pokemonSpecies);
                return Ok(pokemonModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured when trying to fetch pokemon : {name}.", ex);
                throw;
            }
        }

        private async Task<PokemonSpecies?> GetPokemon(string name)
        {
            var pokemon = await _pokemonService.GetPokemonDetailsAsync(name);
            if (pokemon == null)
            {
                return null;
            }

            var pokemonSpecies = await _pokemonService.GetPokemonSpeciesAsync(pokemon.Species?.Uri);

            if (pokemonSpecies == null)
            {
                return null;
            }

            return pokemonSpecies;
        }

        private static PokemonModel GetPokemonModel(string name, PokemonSpecies pokemonSpecies)
        {
            var pokemonModel = new PokemonModel
            {
                Name = name,
                Description = pokemonSpecies?.Flavor_Text_Entries?.FirstOrDefault(x => x.Language.Name.ToLower() == "en" && !String.IsNullOrEmpty(x.Flavor_Text)).Flavor_Text,
                Habitat = pokemonSpecies?.Habitat?.Name,
                IsLegendary = pokemonSpecies?.Is_Legendary
            };

            pokemonModel.Description = pokemonModel.Description?.RemoveNewLineCharacters();
            return pokemonModel;
        }
    }
}