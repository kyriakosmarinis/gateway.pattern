using gateway.shared.Models;
using Microsoft.AspNetCore.Mvc;
using service.music.Data;

namespace service.music.Controllers
{
    [ApiController]
    [Route("music")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicRepository _musicRepository;

        public MusicController(IMusicRepository musicRepository) {
            _musicRepository = musicRepository ?? throw new ArgumentNullException(nameof(musicRepository));
        }

        [HttpGet("api")]
        public async Task<Band> GetBandAsync(string bandId) {
            return await _musicRepository.GetBandInfoAsync(bandId);
        }
    }
}

