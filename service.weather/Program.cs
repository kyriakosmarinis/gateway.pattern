using service.weather.Data;
using static OpenWeatherMap.Cache.Enums;
using OpenWeatherMap.Cache.Extensions;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWeatherRepository, WeatherRepository>();
builder.Services.AddOpenWeatherMapCache(builder.Configuration["AppSettings:WeatherApiKey"], 9_500, FetchMode.AlwaysUseLastMeasuredButExtendCache, 300_000);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

