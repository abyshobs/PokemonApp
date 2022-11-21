using PokemonApi.Repository;
using PokemonApi.Services;

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
