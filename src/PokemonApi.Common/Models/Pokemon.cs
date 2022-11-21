namespace PokemonApi.Common.Models
{
    public class Pokemon
    {
        public PokemonSpeciesLink? Species { get; set; }
        public int? Id { get; set; }
    }

    public class PokemonSpeciesLink
    {
        public string? Name { get; set; }
        public Uri? Url { get; set; }
    }

    public class PokemonSpecies
    {
        public PokemonHabitat? Habitat { get; set; }
        public bool? Is_Legendary { get; set; }
        public List<FlavorText>? Flavor_Text_Entries { get; set; }
    }

    public class PokemonHabitat
    {
        public string? Name { get; set; }
    }

    public class FlavorText
    {
        public string? Flavor_Text { get; set; }
        public Language? Language { get; set; }
    }

    public class Language
    {
        public string? Name { get; set; }
    }
}
