using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HeroesDatabase.DataBaseContext;
using HeroesDatabase.Services;
using HeroesDatabase.Services.InterfaceService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeatherDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherContext") ?? throw new InvalidOperationException("Connection string 'WeatherDatabaseContext' not found.")));

// Add services to the container.

builder.Services.AddScoped<IWeatherService, WeatherService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
