using gateway.shared.Models;

namespace service.weather.Data
{
	public interface IWeatherRepository
	{
        Task<Weather> GetWeatherAsync();
        Task<string> GetWeatherHttpAsync();
    }
}

