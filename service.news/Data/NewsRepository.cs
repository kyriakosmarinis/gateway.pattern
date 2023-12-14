using System;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace service.news.Data
{
	public class NewsRepository : INewsRepository
    {
        private readonly IConfiguration _configuration;
        
        public NewsRepository(IConfiguration configuration)
		{
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<Article>> GetNewsAsync() {
            var newsApiClient = new NewsApiClient(_configuration.GetSection("AppSettings")["NewsApiKey"]);

            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest {
                Q = "Apple",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2023, 12, 12),
                To = DateTime.Today,
                PageSize = 3
            });

            if (articlesResponse.Status == Statuses.Ok) {
                return articlesResponse.Articles;
            }

            return new List<Article>();
        }
    }
}

