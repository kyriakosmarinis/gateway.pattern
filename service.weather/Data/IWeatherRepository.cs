using System;
using service.weather.Models;

namespace service.weather.Data
{
	public interface IWeatherRepository
	{
		IEnumerable<Weather> GetWeather();//todo remame
	}
}

