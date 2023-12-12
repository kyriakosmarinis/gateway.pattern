using System;
using service.news.Models;

namespace service.news.Data
{
	public interface INewsRepository
	{
        IEnumerable<News> GetNews();
    }
}

