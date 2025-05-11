using FluentAssertions.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using System;
using Weather.Data.DataBaseContext;
using Weather.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherContext") ?? throw new InvalidOperationException("Connection string 'WeatherContext' not found.")));

builder.Services.AddScoped<IWeatherService,WeatherService>();

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
