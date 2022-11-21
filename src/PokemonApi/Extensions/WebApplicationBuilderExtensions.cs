using PokemonApi.Repository;
using PokemonApi.Repository.Factories;
using PokemonApi.Repository.Interfaces;
using PokemonApi.Services;
using PokemonApi.Services.Interfaces;

namespace PokemonApi.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureHttpClientFactories(this WebApplicationBuilder builder)
        {

            builder.Services.AddHttpClient("PokeApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["APIs:PokemonAPI"]);
            });

            builder.Services.AddHttpClient("FunTranslationsApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["APIs:FunTranslationsAPI"]);
            });
        }

        public static void ConfigureRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IPokemonRepository, PokemonRepository>();
            builder.Services.AddTransient<ITranslationRespository, YodaTranslator>();
            builder.Services.AddTransient<ITranslationRespository, ShakespeareTranslator>();
        }

        public static void ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IPokemonService, PokemonService>();
            builder.Services.AddTransient<ITranslationService, TranslationService>();
        }
    }
}
