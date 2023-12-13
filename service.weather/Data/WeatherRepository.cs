using System;
using Newtonsoft.Json;
using OpenWeatherMap.Cache;
using service.weather.Models;

namespace service.weather.Data
{
	public class WeatherRepository : IWeatherRepository
    {
        private readonly IOpenWeatherMapCache _openWeatherMapCache;
        
		public WeatherRepository(IOpenWeatherMapCache openWeatherMapCache) {
            _httpClient = new HttpClient();
            _openWeatherMapCache = openWeatherMapCache ?? throw new ArgumentNullException(nameof(openWeatherMapCache));
        }

        public async Task<IEnumerable<OpenWeatherMap.Cache.Models.WeatherCondition>> GetWeatherAsync() {
            try {
                var locationQuery = new OpenWeatherMap.Cache.Models.ZipCode("94040", "us");
                var readings = await _openWeatherMapCache.GetReadingsAsync(locationQuery);

                if (readings.IsSuccessful) return readings.Weather;
                else {
                    var apiErrorCode = readings.Exception?.ApiErrorCode;
                    var apiErrorMessage = readings.Exception?.ApiErrorMessage;

                    throw new Exception($"OpenWeatherMap API error: Code {apiErrorCode}, Message: {apiErrorMessage}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return Enumerable.Empty<OpenWeatherMap.Cache.Models.WeatherCondition>();
            }
        }

        #region http
        private const string _weatherApiKey = "2a74a221dbc2486457abb63b67ae8f7b";
        private const string _weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={"Athens"}&appid={_weatherApiKey}";
        private readonly HttpClient _httpClient;

        public async Task<string> GetWeatherHttpAsync()
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(_weatherUrl);

            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                return json;
            }
            return "error";
        }
        #endregion
    }
}

