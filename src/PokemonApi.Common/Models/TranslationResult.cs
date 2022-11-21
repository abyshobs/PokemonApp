namespace PokemonApi.Common.Models
{
    public class TranslationResult
    {
        public Success? Success { get; set; }
        public TranslationContent? Contents { get; set; }
    }

    public class TranslationContent
    {
        public string? Translated { get; set; }
    }

    public class Success
    {
        public int Total { get; set; }
    }
}
