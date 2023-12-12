using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gateway.api.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather() => await ProxyTo("https://localhost:7186/weather");

        [HttpGet]
        public async Task<IActionResult> GetNews() => await ProxyTo("https://localhost:7110/news");

        private async Task<ContentResult> ProxyTo(string url) => Content(await _httpClient.GetStringAsync(url));
    }
}

