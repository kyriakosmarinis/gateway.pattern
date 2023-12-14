namespace gateway.shared.Models
{
	public class Weather
	{
        public string CityName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime FetchedTime { get; set; }
        public double TemperatureC { get; set; }
    }
}

