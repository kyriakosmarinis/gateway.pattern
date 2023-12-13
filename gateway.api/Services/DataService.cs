using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;

namespace gateway.api.Services
{
    public class User {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserData Data { get; set; } = new UserData();
    }

    public class UserData {
        public string Weather { get; set; } = string.Empty;
        public string News { get; set; } = string.Empty;
    }


	public class DataService {
        private readonly HttpClient _httpClient;

        public DataService(IHttpClientFactory httpClientFactory) {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> GetDataAsync() {
            var weatherTask = GetWeatherDataAsync();
            var newsTask = GetNewsDataAsync();

            await Task.WhenAll(weatherTask, newsTask);
            var weatherContent = await weatherTask;
            var newsContent = await newsTask;

            var user = new User();
            user.Id = new Guid();
            user.Email = $"{user.Id}@mail.com";
            user.Data.Weather = weatherContent.Content;
            user.Data.News = newsContent.Content;

            //var combinedResult = new {
            //    //Weather = weatherContent,
            //    //News = newsContent
            //};

            return new JsonResult(user);//combinedResult);
        }

        private async Task<ContentResult> GetWeatherDataAsync(string url = "https://localhost:7186/weather/api")
        {
            try {
                var content = await _httpClient.GetStringAsync(url);

                return new ContentResult {
                    Content = content,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (HttpRequestException) {
                return new ContentResult {
                    Content = "Error occurred while fetching data.",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private async Task<ContentResult> GetNewsDataAsync(string url = "https://localhost:7110/news/api")
        {
            try {
                var content = await _httpClient.GetStringAsync(url);

                return new ContentResult {
                    Content = content,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (HttpRequestException) {
                return new ContentResult {
                    Content = "Error occurred while fetching data.",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        #region proxy
        /*private async Task<ContentResult> ProxyTo(string url) => Content(await _httpClient.GetStringAsync(url));

        [HttpGet("all")]
        public async Task<IActionResult> GetData() {
            var weatherTask = ProxyTo("https://localhost:7186/weather/api");
            var newsTask = ProxyTo("https://localhost:7110/news/api");

            await Task.WhenAll(weatherTask, newsTask);
            var weatherContent = await weatherTask;
            var newsContent = await newsTask;

            var combinedResult = new {
                Weather = weatherContent,
                News = newsContent
            };

            return new JsonResult(combinedResult);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetDataAsync() {
            var weatherTask = Task.Run(() => ProxyTo("https://localhost:7186/weather/api"));
            var newsTask = Task.Run(() => ProxyTo("https://localhost:7110/news/api"));

            var weatherContent = await weatherTask;
            var newsContent = await newsTask;

            return Ok(new { Weather = weatherContent, News = newsContent });
        }*/
        #endregion
    }
}

