using OpenWeatherMap.Cache.Models;

namespace service.weather.Data
{
	public interface IWeatherRepository
	{
        Task<IEnumerable<WeatherCondition>> GetWeatherAsync();
        Task<string> GetWeatherHttpAsync();
    }
}

