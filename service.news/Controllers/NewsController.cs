using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using service.news.Data;

namespace service.news.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository) {
            _newsRepository = newsRepository ?? throw new ArgumentNullException(nameof(newsRepository));
        }

        [HttpGet("api")]
        public async Task<IEnumerable<Article>> GetNewsAsync(string q) {
            return await _newsRepository.GetNewsAsync(q);
        }
    }
}

