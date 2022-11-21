using PokemonApi.Repository;
using PokemonApi.Repository.Factories;
using PokemonApi.Repository.Interfaces;
using PokemonApi.Services;
using PokemonApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddHttpClient("PokeApi", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["APIs:PokemonAPI"]);
});

builder.Services.AddHttpClient("FunTranslationsApi", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["APIs:FunTranslationsAPI"]);
});

builder.Services.AddTransient<IPokemonRepository, PokemonRepository>();
builder.Services.AddTransient<IPokemonService, PokemonService>();
builder.Services.AddTransient<ITranslationService, TranslationService>();
builder.Services.AddTransient<ITranslationRespository, YodaTranslator>();
builder.Services.AddTransient<ITranslationRespository, ShakespeareTranslator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
