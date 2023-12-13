using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using service.news.Data;
using service.news.Models;

namespace service.news.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository) {
            _newsRepository = newsRepository ?? throw new ArgumentNullException(nameof(newsRepository));
        }

        [HttpGet("api")]
        public async Task<string> GetNewsAsync() {
            return await _newsRepository.GetNewsAsync();
        }
    }
}

