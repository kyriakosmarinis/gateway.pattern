using gateway.shared.Models;

namespace service.weather.Data
{
	public interface IWeatherRepository
	{
        Task<Weather> GetWeatherAsync(double lat, double lon);
        Task<string> GetWeatherHttpAsync();
    }
}

