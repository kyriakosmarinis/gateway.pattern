using NewsAPI.Models;

namespace service.news.Data
{
	public interface INewsRepository
	{
        Task<IEnumerable<Article>> GetNewsAsync(string q);
    }
}

