using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gateway.api.Controllers
{
    [ApiController]
    [Route("gateway")]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProxyController(IHttpClientFactory httpClientFactory) {
            _httpClient = httpClientFactory.CreateClient();
        }

        private async Task<ContentResult> ProxyTo(string url) => Content(await _httpClient.GetStringAsync(url));

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
        }
    }
}

