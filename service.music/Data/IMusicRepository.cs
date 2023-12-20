using gateway.shared.Models;

namespace service.music.Data
{
	public interface IMusicRepository
	{
        Task<string> GetTokenAsync();
        Task<Band> GetBandInfoAsync(string bandId);
    }
}

