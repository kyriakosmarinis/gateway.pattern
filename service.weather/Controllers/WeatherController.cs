﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gateway.shared.Models;
using Microsoft.AspNetCore.Mvc;
using service.weather.Data;

namespace service.weather.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : ControllerBase
    {
        //private readonly ILogger<WeatherController> _logger;//ILogger<WeatherController> logger
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository) {
            _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        }

        [HttpGet("http")]
        public async Task<string> GetOpenWeatherHttp() {
            var weather = await _weatherRepository.GetWeatherHttpAsync();

            if (weather != null) return (weather);
            return "error";
        }

        [HttpGet("api")]
        public async Task<Weather> GetOpenWeather(double lat = 37.04149096479284, double lon = 22.112488382989756)
        {
            var weather = await _weatherRepository.GetWeatherAsync(lat, lon);

            if (weather != null) return (weather);
            return new Weather();
        }
    }
}

