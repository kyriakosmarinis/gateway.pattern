﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using gateway.shared.Models;
using NewsAPI.Models;

namespace gateway.api.Services
{
	public class DataService {
        private readonly HttpClient _httpClient;

        public DataService(IHttpClientFactory httpClientFactory) {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> GetDataAsync(double lat, double lon, string q, string bandId) {
            var weatherTask = GetWeatherDataAsync(lat, lon);
            var newsTask = GetNewsDataAsync(q);
            var musicTask = GetMusicDataAsync(bandId);

            await Task.WhenAll(weatherTask, newsTask, musicTask);
            var weatherContent = await weatherTask;
            var newsContent = await newsTask;
            var musicContent = await musicTask;

            var combinedResult = new
            {
                Weather = JsonConvert.DeserializeObject<Weather>(weatherContent.Content),
                Music = JsonConvert.DeserializeObject<Band>(musicContent.Content),
                News = JsonConvert.DeserializeObject<List<Article>>(newsContent.Content)                
            };

            return new JsonResult(combinedResult);
        }

        #region Weather
        private async Task<ContentResult> GetWeatherDataAsync(double lat, double lon) {
            string url = $"https://localhost:7186/weather/api?lat={lat}&lon={lon}";

            try {
                var content = await _httpClient.GetStringAsync(url);

                return new ContentResult
                {
                    Content = content,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (HttpRequestException)
            {
                return new ContentResult
                {
                    Content = "Error occurred while fetching data.",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

        #region News
        private async Task<ContentResult> GetNewsDataAsync(string q)
        {
            string url = $"https://localhost:7110/news/api?q={q}";

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
        #endregion

        #region Music
        private async Task<ContentResult> GetMusicDataAsync(string bandId)
        {
            string url = $"https://localhost:7015/music/api?bandId={bandId}";

            try
            {
                var content = await _httpClient.GetStringAsync(url);

                return new ContentResult
                {
                    Content = content,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (HttpRequestException)
            {
                return new ContentResult
                {
                    Content = "Error occurred while fetching data.",
                    ContentType = "text/plain",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
        #endregion

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

