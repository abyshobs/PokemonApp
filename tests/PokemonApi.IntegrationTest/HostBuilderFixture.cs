using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace PokemonApi.IntegrationTest
{
    public class HostBuilderFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public T GetService<T>() where T : class
        {
            return Services.GetRequiredService<T>();
        }
    }
}
