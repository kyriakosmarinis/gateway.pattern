using gateway.shared.Models;
using OpenWeatherMap.Cache;

namespace service.weather.Data
{
	public class WeatherRepository : IWeatherRepository
    {
        private readonly IOpenWeatherMapCache _openWeatherMapCache;
        
		public WeatherRepository(IOpenWeatherMapCache openWeatherMapCache) {
            _httpClient = new HttpClient();
            _openWeatherMapCache = openWeatherMapCache ?? throw new ArgumentNullException(nameof(openWeatherMapCache));
        }

        public async Task<Weather> GetWeatherAsync() {
            try {
                var locationQuery = new OpenWeatherMap.Cache.Models.Location(37.04149096479284, 22.112488382989756);
                var readings = await _openWeatherMapCache.GetReadingsAsync(locationQuery);

                if (readings.IsSuccessful) {
                    var weather = new Weather {
                        CityName = readings.CityName,
                        CountryCode = readings.CountryCode,
                        Description = readings.Weather[0].Description,
                        FetchedTime = readings.FetchedTime,
                        TemperatureC = readings.Temperature.DegreesCelsius
                    };

                    return weather;
                }
                else {
                    var apiErrorCode = readings.Exception?.ApiErrorCode;
                    var apiErrorMessage = readings.Exception?.ApiErrorMessage;

                    throw new Exception($"OpenWeatherMap API error: Code {apiErrorCode}, Message: {apiErrorMessage}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return new Weather();
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

