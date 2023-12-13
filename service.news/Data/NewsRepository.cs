using System;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using service.news.Models;

namespace service.news.Data
{
	public class NewsRepository : INewsRepository
    {
        private readonly IConfiguration _configuration;
        
        public NewsRepository(IConfiguration configuration)
		{
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
        }

        public async Task<string> GetNewsAsync() {
            var newsApiClient = new NewsApiClient(_configuration.GetSection("AppSettings")["NewsApiKey"]);

            var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest {
                Q = "Apple",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2023, 12, 12),
                To = DateTime.Today
            });

            if (articlesResponse.Status == Statuses.Ok) {
                Console.WriteLine(articlesResponse.TotalResults);
                return articlesResponse.Articles[0].Description;

                //foreach (var article in articlesResponse.Articles)
                //{
                //    Console.WriteLine(article.Title);
                //    Console.WriteLine(article.Author);
                //    Console.WriteLine(article.Description);
                //    Console.WriteLine(article.Url);
                //    Console.WriteLine(article.PublishedAt);
                //}
            }

            return "error";
        }
    }
}

