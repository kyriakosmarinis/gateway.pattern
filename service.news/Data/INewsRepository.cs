using System;
using NewsAPI.Models;
using service.news.Models;

namespace service.news.Data
{
	public interface INewsRepository
	{
        //Task<IEnumerable<Article>> GetNewsAsync();
        Task<string> GetNewsAsync();
    }
}

