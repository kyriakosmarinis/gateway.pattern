using gateway.shared.Models;

namespace service.music.Data
{
	public interface IMusicRepository
	{
        Task<Band> GetBandAsync();
    }
}

