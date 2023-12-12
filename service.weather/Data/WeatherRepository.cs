using System;
using service.weather.Models;

namespace service.weather.Data
{
	public class WeatherRepository : IWeatherRepository
    {
        private IEnumerable<Weather> _weather { get; set; }

		public WeatherRepository() {
            _weather = new[] {
                new Weather{ TemperatureC = 23 },
                new Weather{ TemperatureC = 32 }
            };
		}

        public IEnumerable<Weather> GetWeather() => _weather;
    }
}

