using System;
using System.Net.Http;
using System.Threading.Tasks;
using gateway.shared.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace service.music.Data
{
    public class MusicRepository : IMusicRepository
    {
        private readonly IConfiguration _configuration;
       
        public MusicRepository(IConfiguration configuration) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Band> GetBandAsync(string bandId) {
            using HttpClient client = new();
            string tokenEndpoint = "https://accounts.spotify.com/api/token";

            var requestData = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _configuration.GetSection("AppSettings")["MusicClientId"]),
                new KeyValuePair<string, string>("client_secret", _configuration.GetSection("AppSettings")["MusicClientSecret"])
            });

            HttpResponseMessage response = await client.PostAsync(tokenEndpoint, requestData);

            if (response.IsSuccessStatusCode) {
                string apiUrl = $"https://api.spotify.com/v1/artists/{bandId}";
                //string apiUrl = "https://api.spotify.com/v1/artists/4Z8W4fKeB5YxbusRsdQVPb";


                string responseContent = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseContent);
                string accessToken = json["access_token"].ToString();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                HttpResponseMessage httpResponse = await client.GetAsync(apiUrl);

                if (httpResponse.IsSuccessStatusCode) {
                    string responseData = await httpResponse.Content.ReadAsStringAsync();
                    JObject jsonData = JObject.Parse(responseData);

                    var band = new Band {
                        Name = jsonData["name"].ToString(),
                        Type = jsonData["type"].ToString(),
                        Popularity = jsonData["popularity"].ToString()
                    };

                    return band;
                }
                else {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                return new Band();
            }
            else {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new Band();
            }
        }

    }
}

