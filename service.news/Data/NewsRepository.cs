using System;
using service.news.Models;

namespace service.news.Data
{
	public class NewsRepository : INewsRepository
    {
        private IEnumerable<News> _news { get; set; }

        public NewsRepository()
		{
            _news = new[] {
                new News{ Description = "Hello world, welcome" },
                new News{ Description = "this is new info" }
            };
        }

        public IEnumerable<News> GetNews() => _news;
    }
}

